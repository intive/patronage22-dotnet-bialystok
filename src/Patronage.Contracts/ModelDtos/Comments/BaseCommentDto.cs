namespace Patronage.Contracts.ModelDtos.IssuesComments
{
    public class BaseCommentDto
    {
        public string Content { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
