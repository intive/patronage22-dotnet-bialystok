using Patronage.Api;

namespace Patronage.Models
{
    public class Project : ICreatable, IModifable
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Issue>? Issues { get; set; }
    }
}