using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IIssueService : IEntityService<Issue>
    {
        IQueryable<Issue> GetAllIssues();
        int Create(Issue dto);
        void Delete(int issueId);
        void Update(int issueId, BaseIssueDto dto);
        void LightUpdate(int issueId, BaseIssueDto dto);
    }
}
