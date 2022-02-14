using MediatR;
using Patronage.Api.MediatR.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Handler.Board
{
    public class UpdateLightCommandHandler : IRequestHandler<UpdateBoardLightCommand, bool>
    {
        public readonly IBoardService boardService;

        public UpdateLightCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<bool> Handle(UpdateBoardLightCommand request, CancellationToken cancellationToken)
        {
            return await boardService.UpdateBoardLightAsync(request.Data);
        }
    }
}
