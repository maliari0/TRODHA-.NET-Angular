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
            // Web projesinin kök dizini + uploads klasörü
            _baseUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            // Uploads klasörü yoksa oluþtur
            if (!Directory.Exists(_baseUploadPath))
                Directory.CreateDirectory(_baseUploadPath);
        }

        public async Task<(string fileName, string filePath, string contentType)> SaveFileAsync(IFormFile file, string subDirectory)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Geçersiz dosya");

            if (!IsValidImageFile(file))
                throw new ArgumentException("Geçersiz görsel dosyasý. Sadece jpeg, png, gif, bmp ve tiff formatlarý desteklenmektedir.");
            
            // Alt dizin yoksa oluþtur
            var uploadPath = Path.Combine(_baseUploadPath, subDirectory);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            
            // Benzersiz dosya adý oluþtur
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadPath, newFileName);
            
            // Dosyayý kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // URL için web yolunu döndür
            var webPath = Path.Combine("uploads", subDirectory, newFileName).Replace("\\", "/");
            
            return (newFileName, webPath, file.ContentType);
        }

        public async Task DeleteFileAsync(string filePath)
        {
            // filePath web yolu olduðu için gerçek yola çevir
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
