using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Functions.Commands.Board.Delete
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public DeleteBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            return await boardService.DeleteBoardAsync(request.Id);
        }
    }
}
