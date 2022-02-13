using MediatR;
using Patronage.Api.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Handlers.Handlers.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public CreateBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<bool> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.CreateBoard(request.Data));
        }
    }
}
