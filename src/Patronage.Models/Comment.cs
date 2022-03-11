using Patronage.Api;

namespace Patronage.Models
{
    public class Comment : ICreatable, IModifable
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
