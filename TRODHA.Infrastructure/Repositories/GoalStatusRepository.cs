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
    public class GoalStatusRepository : Repository<GoalStatus>, IGoalStatusRepository
    {
        public GoalStatusRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GoalStatus>> GetByGoalIdAsync(int goalId)
        {
            return await _context.GoalStatuses
                .Include(gs => gs.Goal)
                .Where(gs => gs.GoalId == goalId)
                .OrderByDescending(gs => gs.RecordDate)
                .ThenByDescending(gs => gs.RecordTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<GoalStatus>> GetByUserIdAsync(int userId)
        {
            return await _context.GoalStatuses
                .Include(gs => gs.Goal)
                .Where(gs => gs.UserId == userId)
                .OrderByDescending(gs => gs.RecordDate)
                .ThenByDescending(gs => gs.RecordTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<GoalStatus>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _context.GoalStatuses
                .Include(gs => gs.Goal)
                .Where(gs => gs.UserId == userId &&
                            gs.RecordDate >= startDate &&
                            gs.RecordDate <= endDate)
                .OrderByDescending(gs => gs.RecordDate)
                .ThenByDescending(gs => gs.RecordTime)
                .ToListAsync();
        }

        public async Task<int> GetCompletedCountForGoalAsync(int goalId, DateTime startDate, DateTime endDate)
        {
            return await _context.GoalStatuses
                .CountAsync(gs => gs.GoalId == goalId &&
                                gs.IsCompleted &&
                                gs.RecordDate >= startDate &&
                                gs.RecordDate <= endDate);
        }
    }
}
