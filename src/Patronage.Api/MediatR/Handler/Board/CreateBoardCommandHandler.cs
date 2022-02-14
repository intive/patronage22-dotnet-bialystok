using MediatR;
using Patronage.Api.MediatR.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Handler.Board
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public CreateBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<bool> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            return await boardService.CreateBoardAsync(request.Data);
        }
    }
}
