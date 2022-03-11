using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Board.Commands.UpdateLight
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
            return await boardService.UpdateBoardLightAsync(request.Data, request.Id);
        }
    }
}
