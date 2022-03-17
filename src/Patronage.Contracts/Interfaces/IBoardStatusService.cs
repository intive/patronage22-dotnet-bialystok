﻿using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardStatusService
    {
        IEnumerable<BoardStatusDto> GetAll();

        IEnumerable<BoardStatusDto> GetById(int boardId, int statusId);

        public bool Create(BoardStatusDto dto);

        public bool Delete(int boardId, int statusId);
    }
}