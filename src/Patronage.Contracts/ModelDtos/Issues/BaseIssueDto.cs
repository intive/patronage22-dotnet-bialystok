using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class BaseIssueDto
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
        public string? AssignUserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseIssueDto(Issue issue)
        {
            Alias = issue.Alias;
            Name = issue.Name;
            Description = issue.Description;
            ProjectId = issue.ProjectId;
            BoardId = issue.BoardId;
            StatusId = issue.StatusId;
            AssignUserId = issue.AssignUserId;
            CreatedOn = issue.CreatedOn;
        }

        public BaseIssueDto()
        {
        }
    }
}