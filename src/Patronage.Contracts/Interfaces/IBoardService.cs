using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardService
    {
        IEnumerable<BoardDto> GetBoards(FilterBoardDto? filter = null);
        bool CreateBoard(BoardDto request);
        BoardDto GetBoardById(int id);
        bool UpdateBoard(BoardDto request);
        bool UpdateBoardLight(PartialBoardDto request);
        bool DeleteBoard(int id);
    }
}
