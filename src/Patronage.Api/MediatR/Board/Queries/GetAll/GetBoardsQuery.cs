using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Queries.GetAll
{
    public class GetBoardsQuery : IRequest<PageResult<BoardDto>>
    {
        public FilterBoardDto filter { get; }

        public GetBoardsQuery(FilterBoardDto filter)
        {
            this.filter = filter;
        }
    }
}