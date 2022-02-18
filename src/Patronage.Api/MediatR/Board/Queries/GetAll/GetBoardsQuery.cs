using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Board.Queries.GetAll
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