using MediatR;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Boards.Commands.DeleteBoard
{
    public class DeleteBoardHandler : IRequestHandler<DeleteBoardCommand>
    {
        private readonly IBoardService _boardService;

        public DeleteBoardHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var boardToDelete = _boardService.GetBoardById(request.id);
            //_boardService.DeleteBoard(boardToDelete);

            return Unit.Value;
        }
    }
}
