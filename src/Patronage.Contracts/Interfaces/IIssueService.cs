using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IIssueService
    {
        List<Issue> GetAll();
        IssueDto GetById(int issueId);
        int Create(BaseIssueDto dto);
        void Delete(int issueId);
        void Update(int issueId, BaseIssueDto dto);
        void LightUpdate(int issueId, BaseIssueDto dto);
    }
}
