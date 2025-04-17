// TRODHA.Infrastructure/Repositories/NoteImageRepository.cs
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
    public class NoteImageRepository : Repository<NoteImage>, INoteImageRepository
    {
        public NoteImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<NoteImage>> GetByNoteIdAsync(int noteId)
        {
            return await _context.NoteImages
                .Where(ni => ni.NoteId == noteId)
                .OrderByDescending(ni => ni.CreatedAt)
                .ToListAsync();
        }
    }
}
