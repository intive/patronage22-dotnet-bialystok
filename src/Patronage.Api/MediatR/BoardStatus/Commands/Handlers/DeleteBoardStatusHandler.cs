﻿using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.BoardStatus.Commands.Handlers
{
    public class DeleteBoardStatusHandler : IRequestHandler<DeleteBoardStatusCommand, bool>
    {
        private readonly IBoardStatusService _boardStatusService;

        public DeleteBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public async Task<bool> Handle(DeleteBoardStatusCommand request, CancellationToken cancellationToken)
        {
            return await _boardStatusService.DeleteAsync(request.boardId, request.statusId);
        }
    }
}