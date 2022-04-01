using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Api.MediatR.BoardStatus.Queries
{
    public class GetAllBoardStatusQuery : IRequest<PageResult<BoardStatusDto>?>
    {
        public FilterBoardStatusDto filter;

        public GetAllBoardStatusQuery(FilterBoardStatusDto filter)
        {
            this.filter = filter;
        }
    }
}