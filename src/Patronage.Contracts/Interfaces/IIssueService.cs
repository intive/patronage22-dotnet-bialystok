using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IIssueService
    {
        IEnumerable<IssueDto> GetAll();
        IssueDto GetById(int issueId);
        int Create(CreateIssueDto dto);
        void Delete(int issueId);
        void Update(int issueId, UpdateIssueDto dto);
    }
}
