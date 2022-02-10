using MediatR;
using Patronage.Contracts.Interfaces;


namespace Patronage.Api.MediatR.Boards.Commands.LightUpdateBoard
{
    public class LightUpdateBoardHandler : IRequestHandler<LightUpdateBoardCommand>
    {
        private readonly IBoardService _boardService;

        public LightUpdateBoardHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }



        public Task<Unit> Handle(LightUpdateBoardCommand request, CancellationToken cancellationToken)
        {
            _boardService.UpdateBoard(request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
