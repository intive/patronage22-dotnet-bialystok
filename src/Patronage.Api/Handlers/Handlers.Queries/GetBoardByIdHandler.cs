using MediatR;
using Patronage.Api.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Handlers.Handlers.Queries
{
    public class GetBoardByIdHandler : IRequestHandler<GetBoardByIdQuery, BoardDto>
    {
        private readonly IBoardService boardService;

        public GetBoardByIdHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<BoardDto> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.GetBoardById(request.Id));
        }
    }
}
