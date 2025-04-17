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
    public class GoalRepository : Repository<Goal>, IGoalRepository
    {
        public GoalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Goal>> GetUserGoalsAsync(int userId)
        {
            return await _context.Goals
                .Include(g => g.ImportanceLevel)
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetActiveUserGoalsAsync(int userId)
        {
            return await _context.Goals
                .Include(g => g.ImportanceLevel)
                .Where(g => g.UserId == userId && g.IsActive)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetGoalCountByImportanceAsync(int userId, int importanceLevelId)
        {
            return await _context.Goals
                .CountAsync(g => g.UserId == userId && g.ImportanceLevelId == importanceLevelId && g.IsActive);
        }
    }
}
