// TRODHA.Application/Services/Interfaces/INoteImageService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface INoteImageService
    {
        Task<NoteImageDto> UploadImageAsync(int noteId, int userId, IFormFile file);
        Task<IEnumerable<NoteImageDto>> GetByNoteIdAsync(int noteId, int userId);
        Task DeleteAsync(int imageId, int userId);
    }
}
