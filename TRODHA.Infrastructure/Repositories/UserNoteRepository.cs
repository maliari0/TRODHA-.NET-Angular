// TRODHA.Infrastructure/Repositories/UserNoteRepository.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;
using TRODHA.Infrastructure.Data;

namespace TRODHA.Infrastructure.Repositories
{
    public class UserNoteRepository : Repository<UserNote>, IUserNoteRepository
    {
        public UserNoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserNote>> GetUserNotesAsync(int userId)
        {
            return await _context.UserNotes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserNote>> GetUserNotesWithImagesAsync(int userId)
        {
            return await _context.UserNotes
                .Include(n => n.Images)
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
