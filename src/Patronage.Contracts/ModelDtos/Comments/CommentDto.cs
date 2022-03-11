using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.IssuesComments
{
    public class CommentDto : BaseCommentDto
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }

        public CommentDto(Comment comment) : base(comment)
        {
            Id = comment.Id;
            IssueId = comment.IssueId;
            UserId = comment.UserId;
            ModifiedOn = comment.ModifiedOn;
        }

        public CommentDto()
        {

        }
    }
}
