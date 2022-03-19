using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Api.MediatR.BoardStatus.Queries.Handlers
{
    public class GetAllBoardStatusHandler : IRequestHandler<GetAllBoardStatusQuery, PageResult<BoardStatusDto>>
    {
        private readonly IBoardStatusService _boardStatusService;

        public GetAllBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<PageResult<BoardStatusDto>> Handle(GetAllBoardStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.GetAll(request.filter));
        }
    }
}