using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Issues
{  
    public class BaseIssueDto
    {
        [Required]
        [MaxLength(256)]
        public string Alias { get; set; } = null!;
        [Required]
        [MaxLength(1024)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }

        public BaseIssueDto(Issue I)
        {
            Alias = I.Alias;
            Name = I.Name;
            Description = I.Description;
            ProjectId = I.ProjectId;
            BoardId = I.BoardId;
            StatusId = I.StatusId;
        }
    }
}
