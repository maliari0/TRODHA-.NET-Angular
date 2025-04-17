using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Core.Models
{
    public class ImportanceLevel
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; } = null!;

        // Navigation properties
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}