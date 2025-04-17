using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Navigation
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public ICollection<UserNote> Notes { get; set; } = new List<UserNote>();
        public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
        public ICollection<GoalStatus> GoalStatuses { get; set; } = new List<GoalStatus>();
    }
}