using MediatR;

namespace Patronage.Api.MediatR.Board.Commands.Delete
{
    public class DeleteBoardCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}