// TRODHA.Application/DTOs/FileDto.cs
using Microsoft.AspNetCore.Http;

namespace TRODHA.Application.DTOs
{
    /// <summary>
    /// Dosya yükleme işlemleri için kullanılan DTO sınıfı.
    /// </summary>
    public class FileUploadDto
    {
        /// <summary>
        /// Yüklenecek dosya.
        /// </summary>
        public IFormFile File { get; set; } = null!;
    }
}
