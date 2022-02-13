using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos
{
    public class PartialBoardDto
    {
        public int Id { get; set; }
        public PropInfo<string>? Name { get; set; }
        public PropInfo<string>? Description { get; set; }
        public PropInfo<bool>? IsActive { get; set; }
    }
}
