using Patronage.Contracts.Helpers;

namespace Patronage.Contracts.ModelDtos.Boards
{
    public class FilterBoardDto
    {
        public int? ProjectId { get; set; }
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = PropertyForQuery.AllowedPageSizes[0];
    }
}