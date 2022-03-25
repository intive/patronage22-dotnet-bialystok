using MediatR;
using Patronage.Contracts.ModelDtos.Comments;

namespace Patronage.Api.MediatR.Comment.Commands
{
    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public BaseCommentDto Data { get; set; } = null!;
    }
}