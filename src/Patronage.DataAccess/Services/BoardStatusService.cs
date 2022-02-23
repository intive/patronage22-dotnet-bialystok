using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
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
        public IEnumerable<BoardStatusDto> GetAll()
        {
            var boardSat = _dbContext
                .BoardsStatus
                .ToList();

            var boardStatuses = new List<BoardStatusDto>();
            foreach (var board in boardSat)
            {
                boardStatuses.Add(new BoardStatusDto
                {
                    BoardId = board.BoardId,
                    StatusId = board.StatusId
                });
            }
            return boardStatuses;
        }

        public IEnumerable<BoardStatusDto> GetById(int boardId, int statusId)
        {
            var boardsStatus = _dbContext
                    .BoardsStatus
                    .AsQueryable();

            var boardsStatusDto = new List<BoardStatusDto>();
            if (boardId != 0 && statusId == 0)
            {
                var res = boardsStatus.Where(b => b.BoardId.Equals(boardId))
                            .ToList();
                foreach (var bs in res)
                {
                    boardsStatusDto.Add(new BoardStatusDto
                    {
                        BoardId = bs.BoardId,
                        StatusId = bs.StatusId
                    });
                }
            }
            else if (boardId == 0 && statusId != 0)
            {
                var res = boardsStatus.Where(b => b.StatusId.Equals(statusId))
                                      .ToList();
                foreach (var bs in res)
                {
                    boardsStatusDto.Add(new BoardStatusDto
                    {
                        BoardId = bs.BoardId,
                        StatusId = bs.StatusId
                    });
                }
            }
            else
            {
                var res = boardsStatus.Where(b => b.StatusId.Equals(statusId))
                            .Where(b => b.BoardId.Equals(boardId))
                            .FirstOrDefault();
                if (res != null)
                {
                    boardsStatusDto.Add(new BoardStatusDto()
                    {
                        BoardId = res.BoardId,
                        StatusId = res.StatusId
                    });
                }
            }
            return boardsStatusDto;
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

                if(boardStatus == null)
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
