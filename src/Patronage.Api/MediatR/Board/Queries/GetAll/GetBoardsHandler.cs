using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Board;

namespace Patronage.Api.MediatR.Board.Queries.GetAll
{
    public class GetBoardsHandler : IRequestHandler<GetBoardsQuery, IEnumerable<BoardDto>?>
    {
        private readonly IBoardService boardService;

        public GetBoardsHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<IEnumerable<BoardDto>?> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            return await boardService.GetBoardsAsync(request.filter);
        }
    }
}
