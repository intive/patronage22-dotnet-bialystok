using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardStatusService
    {
        Task<PageResult<BoardStatusDto>?> GetAllAsync(FilterBoardStatusDto filter);

        Task<bool> CreateAsync(BoardStatusDto dto);

        Task<bool> DeleteAsync(int boardId, int statusId);
    }
}