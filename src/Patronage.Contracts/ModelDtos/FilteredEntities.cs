using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Contracts.ModelDtos
{
    public class FilteredEntities
    {
        public IEnumerable<BaseBoardDto> Boards { get; set; } = null!;
        public IEnumerable<ProjectDto> Projects { get; set; } = null!;
        public IEnumerable<BaseIssueDto> Issues { get; set; } = null!;
    }
}