using Microsoft.AspNetCore.Identity;

namespace Patronage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public virtual ICollection<Issue>? Issues { get; set; }
    }
}
