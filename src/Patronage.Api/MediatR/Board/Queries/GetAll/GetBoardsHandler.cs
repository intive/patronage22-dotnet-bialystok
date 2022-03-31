using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Queries.GetAll
{
    public class GetBoardsHandler : IRequestHandler<GetBoardsQuery, PageResult<BoardDto>?>
    {
        private readonly IBoardService boardService;

        public GetBoardsHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<PageResult<BoardDto>?> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            return await boardService.GetBoardsAsync(request.filter);
        }
    }
}