using MediatR;
using Patronage.Api.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Handlers.Handlers.Commands
{
    public class UpdateLightCommandHandler : IRequestHandler<UpdateBoardLightCommand, bool>
    {
        public readonly IBoardService boardService;

        public UpdateLightCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<bool> Handle(UpdateBoardLightCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.UpdateBoardLight(request.Data));
        }
    }
}
