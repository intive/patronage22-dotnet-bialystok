using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Comments;

namespace Patronage.Api.MediatR.Comment.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto?>
    {
        private readonly ICommentService _commentService;

        public CreateCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<CommentDto?> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            return await _commentService.CreateAsync(request.Data);
        }
    }
}
