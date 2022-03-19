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

        public PageResult<BoardStatusDto>? GetAll(FilterBoardStatusDto filter)
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

            var items = boardStatuses.Select(x => new BoardStatusDto(x)).ToList();

            return new PageResult<BoardStatusDto>(items, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public bool Create(BoardStatusDto dto)
        {
            try
            {
                var boardStatus = new BoardStatus
                {
                    BoardId = dto.BoardId,
                    StatusId = dto.StatusId
                };

                _dbContext.BoardsStatus.Add(boardStatus);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) when (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                //TODO: Ask if it should also catch db savechanges exception
                return false;
            }
        }

        public bool Delete(int boardId, int statusId)
        {
            if (boardId != 0 && statusId != 0)
            {
                var boardStatus = _dbContext
                    .BoardsStatus
                    .Where(b => b.BoardId.Equals(boardId))
                    .Where(b => b.StatusId.Equals(statusId))
                    .FirstOrDefault();

                if (boardStatus == null)
                {
                    return false;
                }

                try
                {
                    _dbContext.BoardsStatus.Remove(boardStatus);
                    _dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}