using Patronage.Api;

namespace Patronage.Models
{
    public class Comment : ICreatable, IModifable
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Issue? Issue { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
