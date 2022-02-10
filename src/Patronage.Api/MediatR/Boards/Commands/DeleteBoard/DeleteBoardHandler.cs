using MediatR;
using Patronage.Contracts.Interfaces;


namespace Patronage.Api.MediatR.Boards.Commands.DeleteBoard
{
    public class DeleteBoardHandler : IRequestHandler<DeleteBoardCommand>
    {
        private readonly IBoardService _boardService;

        public DeleteBoardHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }


        
        public Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            _boardService.DeleteBoard(request.id);

            return Task.FromResult(Unit.Value);
        }
    }
}
