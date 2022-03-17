using MediatR;
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

        public Task<bool> Handle(DeleteBoardStatusCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.Delete(request.boardId, request.statusId));
        }
    }
}