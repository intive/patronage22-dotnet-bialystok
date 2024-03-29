﻿using Patronage.Contracts.ModelDtos.BoardsStatus;
using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Boards
{
    public class BoardDto : BaseBoardDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<BoardStatusDto>? BoardStatuses { get; set; }

        public BoardDto(Board board) : base(board)
        {
            Id = board.Id;
            IsActive = board.IsActive;
            ModifiedOn = board.ModifiedOn;
            BoardStatuses = (List<BoardStatusDto>?)board.BoardStatuses;
        }

        public BoardDto()
        {
        }
    }
}