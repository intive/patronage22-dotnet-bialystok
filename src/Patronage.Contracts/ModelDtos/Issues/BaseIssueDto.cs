using Patronage.Models;
using System.ComponentModel.DataAnnotations;

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
        public string Description { get; set; } = null!;
        [Required]
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
    }
}
