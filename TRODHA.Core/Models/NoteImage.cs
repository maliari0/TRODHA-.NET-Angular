using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class NoteImage
    {
        public int ImageId { get; set; }
        public int NoteId { get; set; }
        public string ImagePath { get; set; } = null!;
        public string ImageType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public UserNote Note { get; set; } = null!;
    }
}