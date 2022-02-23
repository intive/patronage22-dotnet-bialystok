using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class BoardStatus
    {
        public int BoardId { get; set; }
        public int StatusId { get; set;}
        public virtual Board Board { get; set; }
        public virtual Status Status { get; set; }
    }
}
