using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardStatusService
    {
        PageResult<BoardStatusDto> GetAll(FilterBoardStatusDto filter);

        public bool Create(BoardStatusDto dto);

        public bool Delete(int boardId, int statusId);
    }
}