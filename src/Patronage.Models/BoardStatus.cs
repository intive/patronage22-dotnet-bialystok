namespace Patronage.Models
{
    public class BoardStatus
    {
        public int BoardId { get; set; }
        public int StatusId { get; set; }
        public virtual Board Board { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}