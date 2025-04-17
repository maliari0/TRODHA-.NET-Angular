using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IGoalStatusRepository : IRepository<GoalStatus>
    {
        Task<IEnumerable<GoalStatus>> GetByGoalIdAsync(int goalId);
        Task<IEnumerable<GoalStatus>> GetByUserIdAsync(int userId);
        Task<IEnumerable<GoalStatus>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<int> GetCompletedCountForGoalAsync(int goalId, DateTime startDate, DateTime endDate);
    }
}
