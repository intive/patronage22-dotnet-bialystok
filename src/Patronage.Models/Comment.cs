namespace Patronage.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Issue Issue { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
