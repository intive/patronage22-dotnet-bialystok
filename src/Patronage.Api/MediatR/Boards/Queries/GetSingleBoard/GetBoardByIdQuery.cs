using MediatR;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Queries
{
    public record GetBoardByIdQuery(int id) : IRequest<BoardDto>;

}
