// TRODHA.Application/DTOs/GoalStatusDto.cs
using System;

namespace TRODHA.Application.DTOs
{
    public class GoalStatusDto
    {
        public int StatusId { get; set; }
        public int GoalId { get; set; }
        public string GoalTitle { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; }
        public TimeSpan RecordTime { get; set; }
        public int? Duration { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateGoalStatusDto
    {
        public int GoalId { get; set; }
        public DateTime RecordDate { get; set; }
        public TimeSpan RecordTime { get; set; }
        public int? Duration { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
    }

    public class GoalStatusReportDto
    {
        public string GoalTitle { get; set; } = null!;
        public int TargetCount { get; set; }
        public int CompletedCount { get; set; }
        public double CompletionPercentage { get; set; }
    }
}
