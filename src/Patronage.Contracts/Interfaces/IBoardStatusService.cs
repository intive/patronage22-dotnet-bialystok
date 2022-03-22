using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardStatusService
    {
        Task<PageResult<BoardStatusDto>?> GetAll(FilterBoardStatusDto filter);

        Task<bool> Create(BoardStatusDto dto);

        Task<bool> Delete(int boardId, int statusId);
    }
}