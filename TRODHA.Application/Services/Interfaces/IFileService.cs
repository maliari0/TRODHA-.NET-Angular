// TRODHA.Application/Services/Interfaces/IFileService.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<(string fileName, string filePath, string contentType)> SaveFileAsync(IFormFile file, string subDirectory);
        Task DeleteFileAsync(string filePath);
        bool IsValidImageFile(IFormFile file);
    }
}
