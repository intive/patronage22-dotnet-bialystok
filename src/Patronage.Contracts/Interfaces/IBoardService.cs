using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>> GetBoardsAsync(FilterBoardDto? filter = null);
        Task<bool> CreateBoardAsync(BoardDto request);
        Task<BoardDto> GetBoardByIdAsync(int id);
        Task<bool> UpdateBoardAsync(BoardDto request);
        Task<bool> UpdateBoardLightAsync(PartialBoardDto request);
        Task<bool> DeleteBoardAsync(int id);
    }
}
