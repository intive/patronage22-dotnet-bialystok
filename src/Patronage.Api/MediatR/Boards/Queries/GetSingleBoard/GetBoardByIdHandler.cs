using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Queries
{
    public record GetBoardByIdHandler : IRequestHandler<GetBoardByIdQuery, BoardDto>
    {
        private readonly IBoardService _boardService;

        public GetBoardByIdHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }



        public Task<BoardDto> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardService.GetBoardById(request.id));
        }
    }
}
