using MediatR;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Queries
{
    public record GetAllBoardsQuery(BoardDto? filterBoard) : IRequest<IEnumerable<BoardDto>>;
}
