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
            
            // Uygulama URL'ini yap�land�rmadan al
            _baseUrl = configuration["AppSettings:BaseUrl"] ?? "https://localhost:5001";
        }

        public async Task<NoteImageDto> UploadImageAsync(int noteId, int userId, IFormFile file)
        {
            // Notun var oldu�unu ve kullan�c�ya ait oldu�unu do�rula
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            if (note == null)
                throw new ApplicationException("Not bulunamad�");
                
            if (note.UserId != userId)
                throw new ApplicationException("Bu nota g�rsel ekleme yetkiniz yok");
            
            // Dosya do�rulamas�
            if (!_fileService.IsValidImageFile(file))
                throw new ApplicationException("Ge�ersiz g�rsel dosyas�. Sadece jpeg, png, gif, bmp ve tiff formatlar� desteklenmektedir.");
                
            // Dosyay� kaydet
            var (fileName, filePath, contentType) = await _fileService.SaveFileAsync(file, $"notes/{noteId}");
            
            // Dosya tipini belirle
            string imageType = contentType.Split('/')[1].ToLowerInvariant();
            if (imageType == "jpeg") imageType = "jpg";
            
            // Veritaban�na kaydet
            var noteImage = new NoteImage
            {
                NoteId = noteId,
                ImagePath = filePath,
                ImageType = imageType,
                CreatedAt = DateTime.Now
            };
            
            await _noteImageRepository.AddAsync(noteImage);
            
            // DTO d�n���m�
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
            // Notun kullan�c�ya ait olup olmad���n� kontrol et
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            if (note == null || note.UserId != userId)
                throw new ApplicationException("Not bulunamad� veya eri�im izniniz yok");
                
            // G�rselleri getir
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
            // G�rsel ve ilgili notu bul
            var image = await _noteImageRepository.GetByIdAsync(imageId);
            if (image == null)
                throw new ApplicationException("G�rsel bulunamad�");
                
            // Notun kullan�c�ya ait olup olmad���n� kontrol et
            var note = await _userNoteRepository.GetByIdAsync(image.NoteId);
            if (note == null || note.UserId != userId)
                throw new ApplicationException("Bu g�rseli silme yetkiniz yok");
                
            // �nce dosyay� sil
            await _fileService.DeleteFileAsync(image.ImagePath);
            
            // Veritaban� kayd�n� sil
            await _noteImageRepository.DeleteAsync(image);
        }
    }
}
