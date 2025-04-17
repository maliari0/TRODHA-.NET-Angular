// TRODHA.Application/Services/Interfaces/IGoalService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IGoalService
    {
        Task<IEnumerable<GoalDto>> GetUserGoalsAsync(int userId);
        Task<IEnumerable<GoalDto>> GetActiveUserGoalsAsync(int userId);
        Task<GoalDto?> GetGoalByIdAsync(int goalId, int userId);
        Task<GoalDto> CreateGoalAsync(int userId, CreateGoalDto createGoalDto);
        Task<GoalDto> UpdateGoalAsync(int goalId, int userId, UpdateGoalDto updateGoalDto);
        Task DeleteGoalAsync(int goalId, int userId);
        Task<bool> ToggleGoalStatusAsync(int goalId, int userId, bool isActive);
        Task<Dictionary<string, int>> GetGoalStatisticsByUserIdAsync(int userId);
    }
}
