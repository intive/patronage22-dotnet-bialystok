﻿using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Comments;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class CommentService : ICommentService
    {
        private readonly TableContext _dbContext;

        public CommentService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageResult<CommentDto>?> GetAllCommentFromIssue(FilterCommentDto filter)
        {
            var baseQuery = _dbContext
                .Comment
                .Where(x => x.IssueId == filter.IssueId)
                .AsQueryable();

            if (!baseQuery.Any())
            {
                return null;
            }

            var totalItemCount = baseQuery.Count();

            var comments = baseQuery
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize);

            var items = await comments.Select(x => new CommentDto(x)).ToListAsync();

            return new PageResult<CommentDto>(items, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public async Task<CommentDto?> CreateAsync(BaseCommentDto dto)
        {
            if (dto is null)
            {
                return null;
            }

            var comment = new Comment
            {
                Content = dto.Content,
                IssueId = dto.IssueId,
                ApplicationUserId = dto.ApplicationUserId,
            };

            _dbContext.Comment.Add(comment);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                var commentDto = new CommentDto(comment);
                return commentDto;
            }
            return null;
        }

        public async Task<bool> UpdateLightAsync(int commentId, PartialCommentDto dto)
        {
            var comment = await GetByIdAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            if (dto.Content is not null)
            {
                comment.Content = dto.Content?.Data ?? comment.Content;
            }

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            var comment = await GetByIdAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            _dbContext.Remove(comment);

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<Comment?> GetByIdAsync(int commentId)
        {
            var result = await _dbContext
                .Comment
                .FirstOrDefaultAsync(x => x.Id == commentId);

            return result;
        }
    }
}