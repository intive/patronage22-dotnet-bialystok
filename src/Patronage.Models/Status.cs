using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        //Navigation properties 
        public virtual ICollection<BoardStatus>? BoardStatuses { get; set; }
    }
}
