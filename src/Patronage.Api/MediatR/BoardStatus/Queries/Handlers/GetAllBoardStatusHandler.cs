using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.BoardStatus.Queries.Handlers
{
    public class GetAllBoardStatusHandler : IRequestHandler<GetAllBoardStatusQuery, IEnumerable<BoardStatusDto>>
    {
        private readonly IBoardStatusService _boardStatusService;

        public GetAllBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<IEnumerable<BoardStatusDto>> Handle(GetAllBoardStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.GetAll());
        }
    }
}