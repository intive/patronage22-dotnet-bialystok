using MediatR;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.BoardStatus.Queries
{
    public class DeleteBoardStatusHandler :IRequestHandler<DeleteBoardStatusCommand, bool>
    {
        private readonly IBoardStatusService _boardStatusService;

        public DeleteBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<bool> Handle(DeleteBoardStatusCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.Delete(request.boardId, request.statusId));
        }
    }
}
