using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Models;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardService : IEntityService<Board>
    {
        Task<PageResult<BoardDto>?> GetBoardsAsync(FilterBoardDto? filter = null);

        Task<BoardDto?> CreateBoardAsync(BoardDto request);

        Task<BoardDto?> GetBoardByIdAsync(int id);

        Task<bool> UpdateBoardAsync(UpdateBoardDto request, int id);

        Task<bool> UpdateBoardLightAsync(PartialBoardDto request, int id);

        Task<bool> DeleteBoardAsync(int id);
    }
}