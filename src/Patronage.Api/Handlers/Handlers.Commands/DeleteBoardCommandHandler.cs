using MediatR;
using Patronage.Api.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Handlers.Handlers.Commands
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public DeleteBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.DeleteBoard(request.Id));
        }
    }
}
