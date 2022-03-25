namespace Patronage.Contracts.ModelDtos.Comments
{
    public class FilterCommentDto
    {
        public int IssueId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}