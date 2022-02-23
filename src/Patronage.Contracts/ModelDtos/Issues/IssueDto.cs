using System.ComponentModel.DataAnnotations;
using Patronage.Models;


namespace Patronage.Contracts.ModelDtos.Issues
{
    public class IssueDto : BaseIssueDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public IssueDto(Issue I) : base(I)
        {
            Id = I.Id;
            CreatedOn = I.CreatedOn;
            ModifiedOn = I.ModifiedOn;
            IsActive = I.IsActive;
        }
    }
}
