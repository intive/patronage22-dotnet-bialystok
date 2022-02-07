using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardService
    {
        IEnumerable<BoardDto> SearchBoard(BoardDto filter);
        bool CreateBoard(BoardDto request);
        BoardDto GetBoardById(int id);
        bool UpdateBoard(BoardDto request);
    }
}
