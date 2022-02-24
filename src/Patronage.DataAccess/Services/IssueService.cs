using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Helpers;
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

        public async Task<PageResult<IssueDto>?> GetAllIssuesAsync(FilterIssueDto filter)
        {
            var baseQuery = _dbContext
                .Issues
                .AsQueryable();

            if (!baseQuery.Any())
            {
                return null;
            }

            baseQuery = baseQuery
                .FilterBy(filter);
            var totalItemCount = baseQuery.Count();

            var issues = baseQuery
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize);

            var items = await issues.ToArrayAsync();

            List<IssueDto> issuesDtos = new List<IssueDto>();
            foreach (var issue in items)
            {
                issuesDtos.Add(new IssueDto()
                {
                    Id = issue.Id,
                    Alias = issue.Alias,
                    Name = issue.Name,
                    Description = issue.Description,
                    ProjectId = issue.ProjectId,
                    BoardId = issue.BoardId,
                    CreatedOn = issue.CreatedOn,
                    ModifiedOn = issue.ModifiedOn,
                    StatusId = issue.StatusId,
                    IsActive = issue.IsActive
                });
            }
            return new PageResult<IssueDto>(issuesDtos, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public async Task<IssueDto?> CreateAsync(IssueDto dto)
        {
            if (dto is null)
            {
                return null;
            }

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

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                dto.Id = issue.Id;
                return dto;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int issueId, BaseIssueDto dto)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null)
            {
                return false;
            }

            issue.Alias = dto.Alias;
            issue.Name = dto.Name;
            issue.Description = dto.Description;
            issue.ProjectId = dto.ProjectId;
            issue.BoardId = dto.BoardId;
            issue.StatusId = dto.StatusId;

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateLightAsync(int issueId, PartialIssueDto dto)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null)
            {
                return false;
            }

            if (dto.Name is not null)
            {
                issue.Name = dto.Name?.Data ?? issue.Name;
            }
            if (dto.Alias is not null)
            {
                issue.Alias = dto.Alias?.Data ?? issue.Alias;
            }
            if (dto.Description is not null)
            {
                issue.Description = dto.Description?.Data ?? issue.Description;
            }
            if (dto.ProjectId is not null)
            {
                issue.ProjectId = dto.ProjectId?.Data ?? issue.ProjectId;
            }
            if (dto.BoardId is not null)
            {
                issue.BoardId = dto.BoardId?.Data ?? issue.BoardId;
            }
            if (dto.StatusId is not null)
            {
                issue.StatusId = dto.StatusId?.Data ?? issue.StatusId;
            }
            if (dto.IsActive is not null)
            {
                issue.IsActive = dto.IsActive?.Data ?? issue.IsActive;
            }

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int issueId)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null)
            {
                return false;
            }

            issue.IsActive = false;

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }


        public async Task<Issue?> GetByIdAsync(int issueId)
        {
            var result = await _dbContext
                .Issues
                .FirstOrDefaultAsync(x => x.Id == issueId);

            return result;
        }
    }
}
