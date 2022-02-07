using MediatR;


namespace Patronage.Api.MediatR.Boards.Commands.DeleteBoard
{
    public record DeleteBoardCommand(int id) : IRequest;
}
