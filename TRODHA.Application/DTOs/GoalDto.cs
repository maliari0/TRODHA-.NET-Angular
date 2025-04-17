// TRODHA.Application/DTOs/GoalDto.cs
using System;

namespace TRODHA.Application.DTOs
{
    public class GoalDto
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Frequency { get; set; }
        public string PeriodUnit { get; set; } = null!;
        public int ImportanceLevelId { get; set; }
        public string ImportanceLevelName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CompletedCount { get; set; } // Bu hafta/ay tamamlanma sayýsý
        public int TargetCount { get { return Frequency; } } // Hedef sayý
    }

    public class CreateGoalDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Frequency { get; set; }
        public string PeriodUnit { get; set; } = null!;
        public int ImportanceLevelId { get; set; }
    }

    public class UpdateGoalDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Frequency { get; set; }
        public string PeriodUnit { get; set; } = null!;
        public int ImportanceLevelId { get; set; }
        public bool IsActive { get; set; }
    }
}
