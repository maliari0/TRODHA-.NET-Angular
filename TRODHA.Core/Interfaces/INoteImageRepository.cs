// TRODHA.Core/Interfaces/INoteImageRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface INoteImageRepository : IRepository<NoteImage>
    {
        Task<IEnumerable<NoteImage>> GetByNoteIdAsync(int noteId);
    }
}
