using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.BoardStatus.Queries.Handlers
{
    public class GetByIdBoardStatusHandler : IRequestHandler<GetByIdBoardStatusQuery, IEnumerable<BoardStatusDto>>
    {
        private readonly IBoardStatusService _boardStatusService;

        public GetByIdBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<IEnumerable<BoardStatusDto>> Handle(GetByIdBoardStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.GetById(request.BoardId, request.StatusId));
        }
    }
}