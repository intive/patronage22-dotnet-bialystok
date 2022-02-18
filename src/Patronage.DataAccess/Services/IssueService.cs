using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.DataAccess.Services
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

        public int Create(Issue issue)
        {
            _dbContext.Issues.Add(issue);
            _dbContext.SaveChanges();

            return issue.Id;
        }

        public void Delete(int issueId)
        {
            var issue = GetById(issueId);

            issue.IsActive = false;

            _dbContext.SaveChanges();
        }

        public IQueryable<Issue> GetAllIssues()
        {
            var issues = _dbContext
                .Issues;

            return issues;
        }

        public void Update(Issue issue)
        {
            _dbContext.SaveChanges();
        }

        public void LightUpdate(int issueId, BaseIssueDto dto)
        {
            var issue = GetById(issueId);

            /* waiting for validator */

            _dbContext.SaveChanges();
        }

        public Issue GetById(int issueId)
        {
            var result = _dbContext
                .Issues
                .FirstOrDefault(x => x.Id == issueId);

            return result;
        }
    }
}
