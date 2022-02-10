using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<BoardStatusDto>GetById(int boardId, int statusId)
        {
            if(boardId !=0 && statusId == 0)
            {
                var boardStatus = _dbContext
                    .BoardsStatus
                    .Where(b => b.BoardId.Equals(boardId))
                    .ToList();
                return _mapper.Map<List<BoardStatusDto>>(boardStatus);

            }
            else if(boardId == 0 && statusId != 0)
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
        public int Create(BoardStatusDto dto)
        {
            var boardStatus = _mapper.Map<BoardStatus>(dto);
            _dbContext.BoardsStatus.Add(boardStatus);
            _dbContext.SaveChanges();
            return boardStatus.BoardId;
        }
        public void Delete(int boardId, int statusId)
        {
            if (boardId != 0 && statusId != 0)
            {
                var boardStatus = _dbContext
                    .BoardsStatus
                    .Where(b => b.BoardId.Equals(boardId))
                    .Where(b => b.StatusId.Equals(statusId));
                    
                _dbContext.BoardsStatus.RemoveRange(boardStatus);
                _dbContext.SaveChanges();
            }
        }
    }   
}
