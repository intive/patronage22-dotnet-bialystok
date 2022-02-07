using MediatR;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Commands.UpdateBoard
{
    public record UpdateBoardCommand(BoardDto dto) : IRequest;
}
