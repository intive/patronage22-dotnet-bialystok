using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Queries
{
    public class GetBoardsQuery : IRequest<IEnumerable<BoardDto>>
    {
        public FilterBoardDto filter { get; }

        public GetBoardsQuery(FilterBoardDto filter)
        {
            this.filter = filter;
        }
    }
}
