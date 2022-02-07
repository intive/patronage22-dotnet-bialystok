using MediatR;
using Patronage.Contracts.Interfaces;


namespace Patronage.Api.MediatR.Boards.Commands.UpdateBoard
{
    public class UpdateBoardHandler : IRequestHandler<UpdateBoardCommand>
    {
        private readonly IBoardService _boardService;

        public UpdateBoardHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }



        public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            _boardService.UpdateBoard(request.dto);

            return Unit.Value;
        }
    }
}
