using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IUserNoteRepository : IRepository<UserNote>
    {
        Task<IEnumerable<UserNote>> GetUserNotesAsync(int userId);
        Task<IEnumerable<UserNote>> GetUserNotesWithImagesAsync(int userId);
    }
}
