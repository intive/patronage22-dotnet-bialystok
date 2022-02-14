using MediatR;

namespace Patronage.Api.MediatR.Commands
{
    public class DeleteBoardCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
