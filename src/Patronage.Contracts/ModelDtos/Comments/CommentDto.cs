namespace Patronage.Contracts.ModelDtos.IssuesComments
{
    public class CommentDto : BaseCommentDto
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
    }
}
