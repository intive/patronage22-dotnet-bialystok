namespace Patronage.Contracts.ModelDtos.Board
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int? BoardId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<BoardStatusDto>? Board_Status { get; set; }

    }
}
