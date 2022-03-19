namespace Patronage.Contracts.ModelDtos.BoardsStatus
{
    public class FilterBoardStatusDto
    {
        public int? BoardId { get; set; }
        public int? StatusId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
