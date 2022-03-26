using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.BoardsStatus;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class BoardStatusService : IBoardStatusService
    {
        private readonly TableContext _dbContext;

        public BoardStatusService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageResult<BoardStatusDto>?> GetAllAsync(FilterBoardStatusDto filter)
        {
            var baseQuery = _dbContext
                .BoardsStatus
                .AsQueryable();

            if (!baseQuery.Any())
            {
                return null;
            }

            baseQuery = baseQuery
                .FilterBy(filter);
            var totalItemCount = baseQuery.Count();

            var boardStatuses = baseQuery
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize);

            var items = await boardStatuses.Select(x => new BoardStatusDto(x)).ToListAsync();

            return new PageResult<BoardStatusDto>(items, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public async Task<bool> CreateAsync(BoardStatusDto dto)
        {
            var boardStatus = new BoardStatus
            {
                BoardId = dto.BoardId,
                StatusId = dto.StatusId
            };

            _dbContext.BoardsStatus.Add(boardStatus);

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(CreateAsync)}");
        }

        public async Task<bool> DeleteAsync(int boardId, int statusId)
        {
            if (boardId != 0 || statusId != 0)
            {
                return false;
            }

            var boardStatus = _dbContext
                .BoardsStatus
                .Where(b => b.BoardId.Equals(boardId))
                .Where(b => b.StatusId.Equals(statusId))
                .FirstOrDefault();

            if (boardStatus == null)
            {
                return false;
            }

            _dbContext.BoardsStatus.Remove(boardStatus);

            if ((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(DeleteAsync)}");
        }
    }
}