namespace Patronage.Contracts.ModelDtos.Comments
{
    public class FilterCommentDto
    {
        public int IssueId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}