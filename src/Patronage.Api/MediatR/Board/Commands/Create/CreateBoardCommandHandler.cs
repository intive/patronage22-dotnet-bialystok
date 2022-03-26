using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Commands.Create
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto?>
    {
        public readonly IBoardService boardService;

        public CreateBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<BoardDto?> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            return await boardService.CreateBoardAsync(request.Data);
        }
    }
}