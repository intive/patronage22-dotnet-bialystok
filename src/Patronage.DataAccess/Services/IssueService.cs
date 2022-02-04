using Patronage.Common.Exceptions;
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

        private readonly TableContext _dbContext;

        public IssueService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateIssueDto dto)
        {

            return 0;
        }

        public void Delete(int issueId)
        {
            var issue = _dbContext
                .Issues
                .FirstOrDefault(x => x.Id == issueId);

            if (issue is null)
            {
                throw new NotFoundException("Issues not found");
            }

            /* To do - read how to change value in DB */

            _dbContext.SaveChanges();
        }

        public void Update(int issueId, UpdateIssueDto dto)
        {
            
        }
    }
}
