// TRODHA.Application/Services/FileService.cs
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TRODHA.Application.Services.Interfaces;

namespace TRODHA.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _baseUploadPath;
        private readonly string[] _allowedImageExtensions = { ".jpeg", ".jpg", ".png", ".gif", ".bmp", ".tiff" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB

        public FileService()
        {
            // Web projesinin k�k dizini + uploads klas�r�
            _baseUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            // Uploads klas�r� yoksa olu�tur
            if (!Directory.Exists(_baseUploadPath))
                Directory.CreateDirectory(_baseUploadPath);
        }

        public async Task<(string fileName, string filePath, string contentType)> SaveFileAsync(IFormFile file, string subDirectory)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Ge�ersiz dosya");

            if (!IsValidImageFile(file))
                throw new ArgumentException("Ge�ersiz g�rsel dosyas�. Sadece jpeg, png, gif, bmp ve tiff formatlar� desteklenmektedir.");
            
            // Alt dizin yoksa olu�tur
            var uploadPath = Path.Combine(_baseUploadPath, subDirectory);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            
            // Benzersiz dosya ad� olu�tur
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadPath, newFileName);
            
            // Dosyay� kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // URL i�in web yolunu d�nd�r
            var webPath = Path.Combine("uploads", subDirectory, newFileName).Replace("\\", "/");
            
            return (newFileName, webPath, file.ContentType);
        }

        public async Task DeleteFileAsync(string filePath)
        {
            // filePath web yolu oldu�u i�in ger�ek yola �evir
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
            
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }

        public bool IsValidImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0 || file.Length > _maxFileSize)
                return false;
                
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedImageExtensions.Contains(extension);
        }
    }
}
