using MediatR;
using Patronage.Contracts.Interfaces;


namespace Patronage.Api.MediatR.Boards.Commands.CreateBoard
{
    public class CreateBoardHandler : IRequestHandler<CreateBoardCommand>
    {
        private readonly IBoardService _boardService;

        public CreateBoardHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }



        public Task<Unit> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            _boardService.CreateBoard(request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
