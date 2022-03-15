namespace Patronage.Contracts.ModelDtos.Board
{
    public class UpdateBoardDto
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
    }
}
