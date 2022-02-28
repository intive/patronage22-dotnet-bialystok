using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class IssueDto : BaseIssueDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public IssueDto(Issue issue) : base(issue)
        {
            Id = issue.Id;
            CreatedOn = issue.CreatedOn;
            ModifiedOn = issue.ModifiedOn;
            IsActive = issue.IsActive;
        }

        public IssueDto()
        {

        }
    }
}
