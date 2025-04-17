// TRODHA.Infrastructure/Repositories/ImportanceLevelRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;
using TRODHA.Infrastructure.Data;

namespace TRODHA.Infrastructure.Repositories
{
    public class ImportanceLevelRepository : Repository<ImportanceLevel>, IImportanceLevelRepository
    {
        public ImportanceLevelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ImportanceLevel?> GetByNameAsync(string levelName)
        {
            return await _context.ImportanceLevels
                .FirstOrDefaultAsync(il => il.LevelName == levelName);
        }
    }
}
