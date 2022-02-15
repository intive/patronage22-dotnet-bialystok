using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Queries.Board.GetAll
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