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

        public int Create(BaseIssueDto dto)
        {
            var issue = new Issue
            {
                Alias = dto.Alias,
                Name = dto.Name,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                BoardId = dto.BoardId,
                StatusId = dto.StatusId,
                IsActive = true
            };

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

        public void Update(int issueId, BaseIssueDto dto)
        {
            var issue = GetById(issueId);

            issue.Alias = dto.Alias;
            issue.Name = dto.Name;
            issue.Description = dto.Description;
            issue.ModifiedOn = DateTime.UtcNow;
            issue.ProjectId = dto.ProjectId;

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
