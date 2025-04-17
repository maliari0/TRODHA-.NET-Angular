using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IGoalRepository : IRepository<Goal>
    {
        Task<IEnumerable<Goal>> GetUserGoalsAsync(int userId);
        Task<IEnumerable<Goal>> GetActiveUserGoalsAsync(int userId);
        Task<int> GetGoalCountByImportanceAsync(int userId, int importanceLevelId);
    }
}

