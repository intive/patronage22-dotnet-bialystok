using MediatR;

namespace Patronage.Api.Commands
{
    public class DeleteBoardCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
