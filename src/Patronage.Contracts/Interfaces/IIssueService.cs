using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Contracts.Interfaces
{
    public interface IIssueService : IEntityService<Issue>
    {
        IQueryable<Issue> GetAllIssues();
        int Create(Issue issue);
        void Save();
    }
}
