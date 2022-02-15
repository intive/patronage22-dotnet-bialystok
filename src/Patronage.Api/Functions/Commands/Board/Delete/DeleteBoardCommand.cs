using MediatR;

namespace Patronage.Api.Functions.Commands.Board.Delete
{
    public class DeleteBoardCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
