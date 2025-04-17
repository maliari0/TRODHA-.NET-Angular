using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class Recommendation
    {
        public int RecommendationId { get; set; }
        public string Content { get; set; } = null!;
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User? User { get; set; }
    }
}
