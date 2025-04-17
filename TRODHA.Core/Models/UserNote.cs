using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class UserNote
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<NoteImage> Images { get; set; } = new List<NoteImage>();
    }
}
