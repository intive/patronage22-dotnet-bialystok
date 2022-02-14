using AutoMapper;
using Patronage.Common.Exceptions;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
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
            var issue = _mapper.Map<Issue>(dto);
            issue.IsActive = true;
            issue.CreatedOn = DateTime.UtcNow;

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

        public IssueDto GetIssueById(int issueId)
        {
            var issue = GetById(issueId);
            var result = _mapper.Map<IssueDto>(issue);

            return result;
        }

        public void Update(int issueId, BaseIssueDto dto)
        {
            var issue = GetById(issueId);

            issue.Alias = dto.Alias.Data;
            issue.Name = dto.Name.Data;
            issue.Description = dto.Description.Data;
            issue.ModifiedOn = DateTime.UtcNow;
            issue.ProjectId = dto.ProjectId;
            issue.BoardId = dto.BoardId;

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
            var issue = _dbContext
                .Issues
                .FirstOrDefault(x => x.Id == issueId);

            if (issue is null)
            {
                throw new NotFoundException("Issues not found");
            }

            return issue;
        }
    }
}
