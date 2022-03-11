using MediatR;

namespace Patronage.Api.MediatR.Comment.Commands
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
