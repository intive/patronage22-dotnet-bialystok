using MediatR;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Commands.LightUpdateBoard
{
    public record LightUpdateBoardCommand(BoardDto dto) : IRequest;

}
