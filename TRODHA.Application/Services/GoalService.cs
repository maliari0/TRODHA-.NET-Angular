// TRODHA.Application/Services/GoalService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;

namespace TRODHA.Application.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalStatusRepository _goalStatusRepository;
        private readonly IImportanceLevelRepository _importanceLevelRepository;

        public GoalService(
            IGoalRepository goalRepository, 
            IGoalStatusRepository goalStatusRepository,
            IImportanceLevelRepository importanceLevelRepository)
        {
            _goalRepository = goalRepository;
            _goalStatusRepository = goalStatusRepository;
            _importanceLevelRepository = importanceLevelRepository;
        }

        public async Task<IEnumerable<GoalDto>> GetUserGoalsAsync(int userId)
        {
            var goals = await _goalRepository.GetUserGoalsAsync(userId);
            var result = new List<GoalDto>();

            foreach (var goal in goals)
            {
                // Son bir haftalýk tamamlanma sayýsýný hesapla
                var completedCount = await _goalStatusRepository.GetCompletedCountForGoalAsync(
                    goal.GoalId, 
                    DateTime.Now.AddDays(-7), 
                    DateTime.Now);

                result.Add(MapGoalToDto(goal, completedCount));
            }

            return result;
        }

        public async Task<IEnumerable<GoalDto>> GetActiveUserGoalsAsync(int userId)
        {
            var goals = await _goalRepository.GetActiveUserGoalsAsync(userId);
            var result = new List<GoalDto>();

            foreach (var goal in goals)
            {
                // Son bir haftalýk tamamlanma sayýsýný hesapla
                var completedCount = await _goalStatusRepository.GetCompletedCountForGoalAsync(
                    goal.GoalId, 
                    DateTime.Now.AddDays(-7), 
                    DateTime.Now);

                result.Add(MapGoalToDto(goal, completedCount));
            }

            return result;
        }

        public async Task<GoalDto?> GetGoalByIdAsync(int goalId, int userId)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            
            if (goal == null || goal.UserId != userId)
                return null;

            // Son bir haftalýk tamamlanma sayýsýný hesapla
            var completedCount = await _goalStatusRepository.GetCompletedCountForGoalAsync(
                goal.GoalId, 
                DateTime.Now.AddDays(-7), 
                DateTime.Now);

            return MapGoalToDto(goal, completedCount);
        }

        public async Task<GoalDto> CreateGoalAsync(int userId, CreateGoalDto createGoalDto)
        {
            // Ýlgili önem seviyesinin varlýðýný kontrol et
            var importanceLevel = await _importanceLevelRepository.GetByIdAsync(createGoalDto.ImportanceLevelId);
            if (importanceLevel == null)
                throw new ApplicationException("Geçersiz önem seviyesi");

            // Sýklýk ve periyot kontrolü
            ValidateFrequencyAndPeriod(createGoalDto.Frequency, createGoalDto.PeriodUnit);

            var goal = new Goal
            {
                UserId = userId,
                Title = createGoalDto.Title,
                Description = createGoalDto.Description,
                Frequency = createGoalDto.Frequency,
                PeriodUnit = createGoalDto.PeriodUnit,
                ImportanceLevelId = createGoalDto.ImportanceLevelId,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _goalRepository.AddAsync(goal);
            return MapGoalToDto(goal, 0);
        }

        public async Task<GoalDto> UpdateGoalAsync(int goalId, int userId, UpdateGoalDto updateGoalDto)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            
            if (goal == null)
                throw new ApplicationException("Hedef bulunamadý");
            
            if (goal.UserId != userId)
                throw new ApplicationException("Bu hedefi düzenleme yetkiniz yok");

            // Ýlgili önem seviyesinin varlýðýný kontrol et
            var importanceLevel = await _importanceLevelRepository.GetByIdAsync(updateGoalDto.ImportanceLevelId);
            if (importanceLevel == null)
                throw new ApplicationException("Geçersiz önem seviyesi");

            // Sýklýk ve periyot kontrolü
            ValidateFrequencyAndPeriod(updateGoalDto.Frequency, updateGoalDto.PeriodUnit);

            goal.Title = updateGoalDto.Title;
            goal.Description = updateGoalDto.Description;
            goal.Frequency = updateGoalDto.Frequency;
            goal.PeriodUnit = updateGoalDto.PeriodUnit;
            goal.ImportanceLevelId = updateGoalDto.ImportanceLevelId;
            goal.IsActive = updateGoalDto.IsActive;
            goal.UpdatedAt = DateTime.Now;

            await _goalRepository.UpdateAsync(goal);

            // Son bir haftalýk tamamlanma sayýsýný hesapla
            var completedCount = await _goalStatusRepository.GetCompletedCountForGoalAsync(
                goal.GoalId, 
                DateTime.Now.AddDays(-7), 
                DateTime.Now);

            return MapGoalToDto(goal, completedCount);
        }

        public async Task DeleteGoalAsync(int goalId, int userId)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            
            if (goal == null)
                throw new ApplicationException("Hedef bulunamadý");
            
            if (goal.UserId != userId)
                throw new ApplicationException("Bu hedefi silme yetkiniz yok");

            await _goalRepository.DeleteAsync(goal);
        }

        public async Task<bool> ToggleGoalStatusAsync(int goalId, int userId, bool isActive)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            
            if (goal == null)
                throw new ApplicationException("Hedef bulunamadý");
            
            if (goal.UserId != userId)
                throw new ApplicationException("Bu hedefi düzenleme yetkiniz yok");

            goal.IsActive = isActive;
            goal.UpdatedAt = DateTime.Now;

            await _goalRepository.UpdateAsync(goal);
            return goal.IsActive;
        }

        public async Task<Dictionary<string, int>> GetGoalStatisticsByUserIdAsync(int userId)
        {
            var statistics = new Dictionary<string, int>();
            
            // Aktif hedef sayýsý
            var activeGoals = await _goalRepository.GetActiveUserGoalsAsync(userId);
            statistics.Add("ActiveGoalsCount", activeGoals.Count());
            
            // Önem seviyelerine göre hedef daðýlýmý
            statistics.Add("LowImportanceCount", await _goalRepository.GetGoalCountByImportanceAsync(userId, 1));
            statistics.Add("MediumImportanceCount", await _goalRepository.GetGoalCountByImportanceAsync(userId, 2));
            statistics.Add("HighImportanceCount", await _goalRepository.GetGoalCountByImportanceAsync(userId, 3));

            return statistics;
        }

        private void ValidateFrequencyAndPeriod(int frequency, string periodUnit)
        {
            // Sýklýk kontrolü
            if (frequency < 1 || frequency > 6)
                throw new ApplicationException("Sýklýk 1 ile 6 arasýnda olmalýdýr");

            // Periyot kontrolü
            if (!new[] { "günde", "haftada", "ayda", "yýlda" }.Contains(periodUnit))
                throw new ApplicationException("Geçersiz periyot birimi. 'günde', 'haftada', 'ayda' veya 'yýlda' olmalýdýr");
        }

        private GoalDto MapGoalToDto(Goal goal, int completedCount)
        {
            return new GoalDto
            {
                GoalId = goal.GoalId,
                UserId = goal.UserId,
                Title = goal.Title,
                Description = goal.Description,
                Frequency = goal.Frequency,
                PeriodUnit = goal.PeriodUnit,
                ImportanceLevelId = goal.ImportanceLevelId,
                ImportanceLevelName = goal.ImportanceLevel?.LevelName ?? string.Empty,
                IsActive = goal.IsActive,
                CreatedAt = goal.CreatedAt,
                UpdatedAt = goal.UpdatedAt,
                CompletedCount = completedCount
            };
        }
    }
}
