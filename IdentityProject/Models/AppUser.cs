using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Models
{
    public class AppUser : IdentityUser
    {

        public Country Country { get; set; }
    }
}
