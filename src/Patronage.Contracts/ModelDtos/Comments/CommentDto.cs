using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Comments
{
    public class CommentDto : BaseCommentDto
    {
        public int Id { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public CommentDto(Comment comment) : base(comment)
        {
            Id = comment.Id;
            ModifiedOn = comment.ModifiedOn;
        }

        public CommentDto()
        {
        }
    }
}