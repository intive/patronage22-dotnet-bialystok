using Microsoft.AspNetCore.Identity;

namespace Patronage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
    }

    public class TokenUser : IdentityUserToken<string>
    {
        public DateTime ValidUntil { get; set; }
    }
}
