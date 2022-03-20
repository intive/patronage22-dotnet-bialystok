using Microsoft.AspNetCore.Identity;

namespace Patronage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public List<Issue>? Issues { get; set; }
        public List<Comment>? Comment { get; set; }
    }

    public class TokenUser : IdentityUserToken<string>
    {
        public DateTime ValidUntil { get; set; }
        public bool IsActive => DateTime.UtcNow >= ValidUntil;
    }
}