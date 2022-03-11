using MediatR;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCommentCommand(int id)
        {
            Id = id;
        }
    }
}
