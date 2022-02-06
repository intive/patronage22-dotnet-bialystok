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
            var issue = _mapper.Map<Issue>(dto);
            issue.IsActive = true;
            issue.CreatedOn = DateTime.UtcNow;

            _dbContext.Issues.Add(issue);
            _dbContext.SaveChanges();

            return issue.Id;
        }

        public void Delete(int issueId)
        {
            var issue = GetIssue(issueId);

            issue.IsActive = false;

            _dbContext.SaveChanges();
        }

        public IEnumerable<IssueDto> GetAll()
        {
            var issue = _dbContext
                .Issues
                .ToList();
            var projectsDto = _mapper.Map<List<IssueDto>>(issue);

            return projectsDto;
        }

        public IssueDto GetById(int issueId)
        {
            var issue = GetIssue(issueId);
            var result = _mapper.Map<IssueDto>(issue);

            return result;
        }

        public void Update(int issueId, UpdateIssueDto dto)
        {
            var issue = GetIssue(issueId);

            issue.Alias = dto.Alias;
            issue.Name = dto.Name;
            issue.Description = dto.Description;
            issue.ModifiedOn = DateTime.UtcNow;
            issue.ProjectId = dto.ProjectId;
            issue.BoardId = dto.BoardId;
            issue.StatusId = dto.StatusId;
            issue.IsActive = dto.IsActive;

            _dbContext.SaveChanges();
        }

        private Issue GetIssue(int issueId)
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
