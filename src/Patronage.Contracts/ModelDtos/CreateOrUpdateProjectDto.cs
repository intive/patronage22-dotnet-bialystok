using Patronage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos
{
    public class CreateOrUpdateProjectDto
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
