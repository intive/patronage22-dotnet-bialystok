﻿using AutoMapper;
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
            var results = await _dbContext
                .Issues
                .ToListAsync();

            if (!results.Any())
            {
                return null;
            }

            if(filter.SearchPhrase != null )
            {
                results = (List<Issue>) results.Where(x => x.Alias.Contains(filter.SearchPhrase) || 
                x.Name.Contains(filter.SearchPhrase) || 
                (x.Description != null && x.Description.Contains(filter.SearchPhrase)));
            }

            var issues = results
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize);

            List<IssueDto> issuesDto = new();
            foreach (var issue in issues)
            {
                issuesDto.Add(new IssueDto(issue));
            }
            return new PageResult<IssueDto>(issuesDto, results.Count, filter.PageSize, filter.PageNumber);
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
