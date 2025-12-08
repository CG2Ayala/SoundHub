using Microsoft.AspNetCore.Identity;

namespace SoundHub.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }
    }
}
