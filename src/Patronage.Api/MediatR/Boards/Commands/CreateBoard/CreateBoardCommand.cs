using MediatR;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Commands.CreateBoard
{
    public record CreateBoardCommand(BoardDto dto) : IRequest;
}
