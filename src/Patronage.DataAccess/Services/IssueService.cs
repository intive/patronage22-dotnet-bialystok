using AutoMapper;
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
        private readonly IMapper _mapper;

        public IssueService(TableContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateIssueDto dto)
        {

            /* create Create issue
               return id issue */

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

            issue.IsActive = false;

            _dbContext.SaveChanges();
        }

        public IEnumerable<IssueDto> GetAll()
        {


            throw new NotImplementedException();
        }

        public IssueDto GetById(int issueId)
        {
            var issue = _dbContext
                .Issues
                .FirstOrDefault(x => x.Id == issueId);

            if (issue is null)
            {
                throw new NotFoundException("Issues not found");
            }

            /* create GetById issue */

            return new IssueDto();
        }

        public void Update(int issueId, UpdateIssueDto dto)
        {
            var issue = _dbContext
                .Issues
                .FirstOrDefault(x => x.Id == issueId);

            if (issue is null)
            {
                throw new NotFoundException("Issues not found");
            }

            /* create Update */

            _dbContext.SaveChanges();
        }
    }
}
