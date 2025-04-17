// TRODHA.Application/Services/Interfaces/IUserNoteService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IUserNoteService
    {
        Task<IEnumerable<UserNoteDto>> GetUserNotesAsync(int userId);
        Task<UserNoteDto?> GetByIdAsync(int noteId, int userId);
        Task<UserNoteDto> CreateAsync(int userId, CreateUserNoteDto createDto);
        Task<UserNoteDto> UpdateAsync(int noteId, int userId, UpdateUserNoteDto updateDto);
        Task DeleteAsync(int noteId, int userId);
    }
}
