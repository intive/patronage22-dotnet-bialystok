using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Comments
{
    public class BaseCommentDto
    {
        public string Content { get; set; } = null!;
        public int IssueId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseCommentDto(Comment comment)
        {
            Content = comment.Content;
            IssueId = comment.IssueId;
            ApplicationUserId = comment.ApplicationUserId;
            CreatedOn = comment.CreatedOn;
        }

#pragma warning disable CS8618
        public BaseCommentDto()
#pragma warning restore CS8618
        {

        }
    }
}
