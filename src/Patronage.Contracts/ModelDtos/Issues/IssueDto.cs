using System.ComponentModel.DataAnnotations;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class IssueDto : BaseIssueDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
