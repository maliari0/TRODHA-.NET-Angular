using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Frequency { get; set; }
        public string PeriodUnit { get; set; } = null!;
        public int ImportanceLevelId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ImportanceLevel ImportanceLevel { get; set; } = null!;
        public ICollection<GoalStatus> Statuses { get; set; } = new List<GoalStatus>();
    }
}
