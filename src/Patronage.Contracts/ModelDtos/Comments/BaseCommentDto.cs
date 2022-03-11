using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Comments
{
    public class BaseCommentDto
    {
        public string Content { get; set; } = null!;
        public int IssueId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseCommentDto(Comment comment)
        {
            Content = comment.Content;
            IssueId = comment.IssueId;
            UserId = comment.UserId;
            CreatedOn = comment.CreatedOn;
        }

        public BaseCommentDto(BaseCommentDto baseComment)
        {
            Content = baseComment.Content;
            IssueId = baseComment.IssueId;
            UserId = baseComment.UserId;
            CreatedOn = baseComment.CreatedOn;
        }

        public BaseCommentDto()
        {

        }
    }
}
