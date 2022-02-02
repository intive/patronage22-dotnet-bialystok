using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos
{
    public class CreateIssueDto
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int BoardId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }



        //public PropInfo<string> Name { get; set; }
        //public PropInfo<string> Description { get; set; }
        //public PropInfo<bool> IsActive { get; set; }
    }
}
