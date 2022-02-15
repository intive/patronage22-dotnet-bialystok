using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Board.Queries.GetSingle
{
    public class GetBoardByIdQuery : IRequest<BoardDto>
    {
        public int Id { get; }

        public GetBoardByIdQuery(int id)
        {
            Id = id;
        }
    }
}