using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class IssueService : IIssueService
    {
        private readonly TableContext _dbContext;

        public IssueService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Issue issue)
        {
            _dbContext.Issues.Add(issue);
            _dbContext.SaveChanges();

            return issue.Id;
        }

        public IQueryable<Issue> GetAllIssues()
        {
            var issues = _dbContext
                .Issues;

            return issues;
        }

        public void Save()
        {
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
