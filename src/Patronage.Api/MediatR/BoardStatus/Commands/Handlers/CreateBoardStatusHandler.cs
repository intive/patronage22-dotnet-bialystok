using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.BoardStatus.Commands.Handlers
{
    public class CreateBoardStatusHandler : IRequestHandler<CreateBoardStatusCommand, bool>
    {
        private readonly IBoardStatusService _boardStatusService;

        public CreateBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public async Task<bool> Handle(CreateBoardStatusCommand request, CancellationToken cancellationToken)
        {
            return await _boardStatusService.CreateAsync(request.Dto);
        }
    }
}