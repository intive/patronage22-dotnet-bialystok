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
        public ApplicationUser? AssignUser { get; set; }

        public BaseIssueDto(Issue isse)
        {
            Alias = isse.Alias;
            Name = isse.Name;
            Description = isse.Description;
            ProjectId = isse.ProjectId;
            BoardId = isse.BoardId;
            StatusId = isse.StatusId;
            AssignUser = isse.AssignUser;
        }

        public BaseIssueDto()
        {

        }
    }
}
