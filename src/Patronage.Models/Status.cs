namespace Patronage.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;

        //Navigation properties
        public virtual ICollection<BoardStatus>? BoardStatuses { get; set; }
    }
}