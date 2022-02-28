using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public List<BoardStatusDto>? Board_Status { get; set; }

    }
}
