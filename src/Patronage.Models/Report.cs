using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public string File { get; set; } = null!;
    }
}
