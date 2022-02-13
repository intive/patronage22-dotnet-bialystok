using AutoMapper;
using MediatR;
using Patronage.Api.Commands;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Handlers.Handlers.Commands
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public UpdateBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<bool> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.UpdateBoard(request.Data));
        }
    }
}
