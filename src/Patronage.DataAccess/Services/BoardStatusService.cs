using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{


    public class BoardStatusService : IBoardStatusService
    {
        private readonly TableContext _dbContext;
        private readonly IMapper _mapper;

        public BoardStatusService(TableContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<BoardStatusDto> GetAll()
        {
            var boardSat = _dbContext
                .BoardsStatus
                .ToList();

            var boardStatuses = _mapper.Map<List<BoardStatusDto>>(boardSat);

            return boardStatuses;

        }
        public IEnumerable<BoardStatusDto> GetById(int boardId, int statusId)
        {
            if (boardId != 0 && statusId == 0)
            {
                var boardStatus = _dbContext
                    .BoardsStatus
                    .Where(b => b.BoardId.Equals(boardId))
                    .ToList();
                return _mapper.Map<List<BoardStatusDto>>(boardStatus);

            }
            else if (boardId == 0 && statusId != 0)
            {
                var boardStatus = _dbContext
                    .BoardsStatus
                    .Where(b => b.StatusId.Equals(statusId))
                    .ToList();
                return _mapper.Map<List<BoardStatusDto>>(boardStatus);

            }
            else
            {
                var boardStatus = _dbContext
                   .BoardsStatus
                   .Where(b => b.BoardId.Equals(boardId))
                   .Where(b => b.StatusId.Equals(statusId));
                return _mapper.Map<List<BoardStatusDto>>(boardStatus);

            }
        }
        public bool Create(BoardStatusDto dto)
        {
            try
            {
                var boardStatus = _mapper.Map<BoardStatus>(dto);
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
                    .Where(b => b.StatusId.Equals(statusId));

                try
                {
                    _dbContext.BoardsStatus.RemoveRange(boardStatus);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    return false;
                }

                
            }
            return true;
        }
    }
}
