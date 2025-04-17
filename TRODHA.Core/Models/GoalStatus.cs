using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class GoalStatus
    {
        public int StatusId { get; set; }
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; }
        public TimeSpan RecordTime { get; set; }
        public int? Duration { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Goal Goal { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
