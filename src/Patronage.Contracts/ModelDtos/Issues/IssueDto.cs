using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class IssueDto : BaseIssueDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<Comment>? Comments { get; set; }

        public IssueDto(Issue issue) : base(issue)
        {
            Id = issue.Id;
            IsActive = issue.IsActive;
            ModifiedOn = issue.ModifiedOn;
            Comments = issue.Comment;
        }

        public IssueDto()
        {
        }
    }
}