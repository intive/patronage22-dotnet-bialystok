using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.BoardsStatus
{
    public class BoardStatusDto
    {
        public int BoardId { get; set; }
        public int StatusId { get; set; }

        public BoardStatusDto(BoardStatus boardStatus)
        {
            BoardId = boardStatus.BoardId;
            StatusId = boardStatus.StatusId;
        }

        public BoardStatusDto()
        {
        }
    }
}