// TRODHA.Application/DTOs/RecommendationDto.cs
using System;

namespace TRODHA.Application.DTOs
{
    public class RecommendationDto
    {
        public int RecommendationId { get; set; }
        public string Content { get; set; } = null!;
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateRecommendationDto
    {
        public string Content { get; set; } = null!;
        public int? UserId { get; set; }
    }

    public class UpdateRecommendationDto
    {
        public string Content { get; set; } = null!;
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
