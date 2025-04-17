// TRODHA.Application/Services/GoalStatusService.cs
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
    public class GoalStatusService : IGoalStatusService
    {
        private readonly IGoalStatusRepository _goalStatusRepository;
        private readonly IGoalRepository _goalRepository;

        public GoalStatusService(
            IGoalStatusRepository goalStatusRepository,
            IGoalRepository goalRepository)
        {
            _goalStatusRepository = goalStatusRepository;
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<GoalStatusDto>> GetByGoalIdAsync(int goalId, int userId)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            if (goal == null || goal.UserId != userId)
                throw new ApplicationException("Geçersiz hedef veya eriþim izniniz yok");

            var statuses = await _goalStatusRepository.GetByGoalIdAsync(goalId);
            return statuses.Select(MapToDto);
        }

        public async Task<IEnumerable<GoalStatusDto>> GetByUserIdAsync(int userId)
        {
            var statuses = await _goalStatusRepository.GetByUserIdAsync(userId);
            return statuses.Select(MapToDto);
        }

        public async Task<IEnumerable<GoalStatusDto>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ApplicationException("Baþlangýç tarihi bitiþ tarihinden sonra olamaz");

            var statuses = await _goalStatusRepository.GetByDateRangeAsync(userId, startDate, endDate);
            return statuses.Select(MapToDto);
        }

        public async Task<GoalStatusDto> CreateAsync(int userId, CreateGoalStatusDto createDto)
        {
            var goal = await _goalRepository.GetByIdAsync(createDto.GoalId);
            
            if (goal == null)
                throw new ApplicationException("Hedef bulunamadý");
            
            if (goal.UserId != userId)
                throw new ApplicationException("Bu hedefe durum ekleme yetkiniz yok");

            if (createDto.RecordDate > DateTime.Now.Date)
                throw new ApplicationException("Gelecek tarihli kayýt eklenemez");

            var goalStatus = new GoalStatus
            {
                GoalId = createDto.GoalId,
                UserId = userId,
                RecordDate = createDto.RecordDate,
                RecordTime = createDto.RecordTime,
                Duration = createDto.Duration,
                IsCompleted = createDto.IsCompleted,
                Notes = createDto.Notes,
                CreatedAt = DateTime.Now
            };

            await _goalStatusRepository.AddAsync(goalStatus);
            
            // Goal entity'sini include edip, dto'ya maplememiz gerekiyor
            var savedStatus = await _goalStatusRepository.GetByIdAsync(goalStatus.StatusId);
            if (savedStatus == null)
                throw new ApplicationException("Durum kaydý oluþturulamadý");
                
            return MapToDto(savedStatus);
        }

        public async Task DeleteAsync(int statusId, int userId)
        {
            var status = await _goalStatusRepository.GetByIdAsync(statusId);
            
            if (status == null)
                throw new ApplicationException("Durum kaydý bulunamadý");
            
            if (status.UserId != userId)
                throw new ApplicationException("Bu durum kaydýný silme yetkiniz yok");

            await _goalStatusRepository.DeleteAsync(status);
        }

        public async Task<IEnumerable<GoalStatusReportDto>> GetUserGoalReportAsync(int userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ApplicationException("Baþlangýç tarihi bitiþ tarihinden sonra olamaz");

            var goals = await _goalRepository.GetUserGoalsAsync(userId);
            var result = new List<GoalStatusReportDto>();

            foreach (var goal in goals)
            {
                var completedCount = await _goalStatusRepository.GetCompletedCountForGoalAsync(
                    goal.GoalId, 
                    startDate, 
                    endDate);
                
                // Hedef periyoduna göre toplam hedef sayýsýný hesapla
                int targetDaysInPeriod = 0;
                
                switch (goal.PeriodUnit)
                {
                    case "günde":
                        targetDaysInPeriod = (int)(endDate - startDate).TotalDays + 1;
                        break;
                    case "haftada":
                        targetDaysInPeriod = (int)Math.Ceiling((endDate - startDate).TotalDays / 7.0);
                        break;
                    case "ayda":
                        // Kabaca ay sayýsýný hesaplama
                        targetDaysInPeriod = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1;
                        break;
                    case "yýlda":
                        targetDaysInPeriod = endDate.Year - startDate.Year + 1;
                        break;
                }

                int targetCount = goal.Frequency * targetDaysInPeriod;
                double completionPercentage = targetCount > 0 ? ((double)completedCount / targetCount) * 100 : 0;

                result.Add(new GoalStatusReportDto
                {
                    GoalTitle = goal.Title,
                    TargetCount = targetCount,
                    CompletedCount = completedCount,
                    CompletionPercentage = Math.Round(completionPercentage, 2)
                });
            }

            return result;
        }

        private GoalStatusDto MapToDto(GoalStatus status)
        {
            return new GoalStatusDto
            {
                StatusId = status.StatusId,
                GoalId = status.GoalId,
                GoalTitle = status.Goal?.Title ?? "Bilinmeyen Hedef",
                UserId = status.UserId,
                RecordDate = status.RecordDate,
                RecordTime = status.RecordTime,
                Duration = status.Duration,
                IsCompleted = status.IsCompleted,
                Notes = status.Notes,
                CreatedAt = status.CreatedAt
            };
        }
    }
}
