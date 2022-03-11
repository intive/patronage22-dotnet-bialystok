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
                .Where(x => x.IsActive == true)
                .Include(x => x.Comment)
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
                issuesDtos.Add(new IssueDto(issue));
            }
            return new PageResult<IssueDto>(issuesDtos, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public async Task<IssueDto?> CreateAsync(BaseIssueDto dto)
        {
            if (dto is null)
            {
                return null;
            }

            var issue = new Issue
            {
                Alias = dto.Alias,
                Name = dto.Name,
                Description = dto?.Description ?? null,
                ProjectId = dto!.ProjectId,
                BoardId = dto?.BoardId ?? null,
                StatusId = dto!.StatusId,
                AssignUserId = dto?.AssignUserId ?? null,
                IsActive = true
            };

            _dbContext.Issues.Add(issue);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                var issueDto = new IssueDto(issue);
                return issueDto;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int issueId, BaseIssueDto dto)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null || !issue.IsActive)
            {
                return false;
            }

            issue.Alias = dto.Alias;
            issue.Name = dto.Name;
            issue.Description = dto.Description;
            issue.ProjectId = dto.ProjectId;
            issue.BoardId = dto.BoardId;
            issue.StatusId = dto.StatusId;
            issue.AssignUserId = dto.AssignUserId;

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateLightAsync(int issueId, PartialIssueDto dto)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null || !issue.IsActive)
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
            if (dto.AssignUserId is not null)
            {
                issue.AssignUserId = dto.AssignUserId?.Data ?? issue.AssignUserId;
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
            if (issue == null || !issue.IsActive)
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

        public async Task<bool> AssignUserAsync(int issueId, string userId)
        {
            var issue = await GetByIdAsync(issueId);
            if (issue == null || !issue.IsActive)
            {
                return false;
            }

            issue.AssignUserId = userId;

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
                .Include(x => x.Comment)
                .FirstOrDefaultAsync(x => x.Id == issueId);

            return result;
        }
    }
}
