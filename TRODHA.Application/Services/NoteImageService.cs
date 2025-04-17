// TRODHA.Application/Services/NoteImageService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;

namespace TRODHA.Application.Services
{
    public class NoteImageService : INoteImageService
    {
        private readonly INoteImageRepository _noteImageRepository;
        private readonly IUserNoteRepository _userNoteRepository;
        private readonly IFileService _fileService;
        private readonly string _baseUrl;

        public NoteImageService(INoteImageRepository noteImageRepository, 
                               IUserNoteRepository userNoteRepository,
                               IFileService fileService,
                               IConfiguration configuration)
        {
            _noteImageRepository = noteImageRepository;
            _userNoteRepository = userNoteRepository;
            _fileService = fileService;
            
            // Uygulama URL'ini yapýlandýrmadan al
            _baseUrl = configuration["AppSettings:BaseUrl"] ?? "https://localhost:5001";
        }

        public async Task<NoteImageDto> UploadImageAsync(int noteId, int userId, IFormFile file)
        {
            // Notun var olduðunu ve kullanýcýya ait olduðunu doðrula
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            if (note == null)
                throw new ApplicationException("Not bulunamadý");
                
            if (note.UserId != userId)
                throw new ApplicationException("Bu nota görsel ekleme yetkiniz yok");
            
            // Dosya doðrulamasý
            if (!_fileService.IsValidImageFile(file))
                throw new ApplicationException("Geçersiz görsel dosyasý. Sadece jpeg, png, gif, bmp ve tiff formatlarý desteklenmektedir.");
                
            // Dosyayý kaydet
            var (fileName, filePath, contentType) = await _fileService.SaveFileAsync(file, $"notes/{noteId}");
            
            // Dosya tipini belirle
            string imageType = contentType.Split('/')[1].ToLowerInvariant();
            if (imageType == "jpeg") imageType = "jpg";
            
            // Veritabanýna kaydet
            var noteImage = new NoteImage
            {
                NoteId = noteId,
                ImagePath = filePath,
                ImageType = imageType,
                CreatedAt = DateTime.Now
            };
            
            await _noteImageRepository.AddAsync(noteImage);
            
            // DTO dönüþümü
            return new NoteImageDto
            {
                ImageId = noteImage.ImageId,
                NoteId = noteImage.NoteId,
                ImagePath = noteImage.ImagePath,
                ImageType = noteImage.ImageType,
                CreatedAt = noteImage.CreatedAt,
                ImageUrl = $"{_baseUrl}/{noteImage.ImagePath}"
            };
        }

        public async Task<IEnumerable<NoteImageDto>> GetByNoteIdAsync(int noteId, int userId)
        {
            // Notun kullanýcýya ait olup olmadýðýný kontrol et
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            if (note == null || note.UserId != userId)
                throw new ApplicationException("Not bulunamadý veya eriþim izniniz yok");
                
            // Görselleri getir
            var images = await _noteImageRepository.GetByNoteIdAsync(noteId);
            
            return images.Select(img => new NoteImageDto
            {
                ImageId = img.ImageId,
                NoteId = img.NoteId,
                ImagePath = img.ImagePath,
                ImageType = img.ImageType,
                CreatedAt = img.CreatedAt,
                ImageUrl = $"{_baseUrl}/{img.ImagePath}"
            });
        }

        public async Task DeleteAsync(int imageId, int userId)
        {
            // Görsel ve ilgili notu bul
            var image = await _noteImageRepository.GetByIdAsync(imageId);
            if (image == null)
                throw new ApplicationException("Görsel bulunamadý");
                
            // Notun kullanýcýya ait olup olmadýðýný kontrol et
            var note = await _userNoteRepository.GetByIdAsync(image.NoteId);
            if (note == null || note.UserId != userId)
                throw new ApplicationException("Bu görseli silme yetkiniz yok");
                
            // Önce dosyayý sil
            await _fileService.DeleteFileAsync(image.ImagePath);
            
            // Veritabaný kaydýný sil
            await _noteImageRepository.DeleteAsync(image);
        }
    }
}
