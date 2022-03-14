using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Contracts.Interfaces
{
    public interface IIssueService : IEntityService<Issue>
    {
        Task<PageResult<IssueDto>?> GetAllIssuesAsync(FilterIssueDto filter);
        Task<IssueDto?> CreateAsync(BaseIssueDto issue);
        Task<bool> UpdateAsync(int issueId, BaseIssueDto dto);
        Task<bool> UpdateLightAsync(int issueId, PartialIssueDto dto);
        Task<bool> DeleteAsync(int issueId);
        Task<bool> AssignUserAsync(int issueId, string userId);
    }
}
