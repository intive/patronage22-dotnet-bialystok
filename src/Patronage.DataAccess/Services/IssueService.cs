using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models.Services
{
    public class IssueService : IIssueService
    {
        public int Create(CreateIssueDto dto)
        {

            return 0;
        }

        public void Delete(int issueId)
        {

        }

        public void Update(int issueId, UpdateIssueDto dto)
        {

        }
    }
}
