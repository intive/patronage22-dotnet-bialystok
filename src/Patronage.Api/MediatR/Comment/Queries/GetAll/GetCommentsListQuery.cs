using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Comments;

namespace Patronage.Api.MediatR.Comment.Queries
{
    public class GetCommentsListQuery : IRequest<PageResult<CommentDto>>
    {
        public FilterCommentDto filter;

        public GetCommentsListQuery(FilterCommentDto filter)
        {
            this.filter = filter;
        }
    }
}
