using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Comments;

namespace Patronage.Api.MediatR.Comment.Queries
{
    public class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, PageResult<CommentDto>?>
    {
        private readonly ICommentService _commentService;

        public GetCommentsListQueryHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<PageResult<CommentDto>?> Handle(GetCommentsListQuery request, CancellationToken cancellationToken)
        {
            return await _commentService.GetAllCommentFromIssue(request.filter);
        }
    }
}