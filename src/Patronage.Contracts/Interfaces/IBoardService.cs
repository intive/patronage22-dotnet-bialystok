using Patronage.Contracts.ModelDtos.Board;
using Patronage.Models;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardService : IEntityService<Board>
    {
        Task<IEnumerable<BoardDto>?> GetBoardsAsync(FilterBoardDto? filter = null);

        Task<BoardDto?> CreateBoardAsync(BoardDto request);

        Task<BoardDto?> GetBoardByIdAsync(int id);

        Task<bool> UpdateBoardAsync(UpdateBoardDto request, int id);

        Task<bool> UpdateBoardLightAsync(PartialBoardDto request, int id);

        Task<bool> DeleteBoardAsync(int id);
    }
}