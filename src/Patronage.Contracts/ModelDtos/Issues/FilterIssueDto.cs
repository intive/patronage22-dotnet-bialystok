namespace Patronage.Contracts.ModelDtos.Issues
{
    public class FilterIssueDto
    {
        public int? BoardId { get; set; }
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}