using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Board;

namespace Patronage.Api.MediatR.Board.Queries.GetSingle
{
    public class GetBoardByIdHandler : IRequestHandler<GetBoardByIdQuery, BoardDto?>
    {
        private readonly IBoardService boardService;

        public GetBoardByIdHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<BoardDto?> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
        {
            return await boardService.GetBoardByIdAsync(request.Id);
        }
    }
}