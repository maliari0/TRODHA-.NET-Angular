// TRODHA.Application/Services/Interfaces/IGoalStatusService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IGoalStatusService
    {
        Task<IEnumerable<GoalStatusDto>> GetByGoalIdAsync(int goalId, int userId);
        Task<IEnumerable<GoalStatusDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<GoalStatusDto>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<GoalStatusDto> CreateAsync(int userId, CreateGoalStatusDto createDto);
        Task DeleteAsync(int statusId, int userId);
        Task<IEnumerable<GoalStatusReportDto>> GetUserGoalReportAsync(int userId, DateTime startDate, DateTime endDate);
    }
}
