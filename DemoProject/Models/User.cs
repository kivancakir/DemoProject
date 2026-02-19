using Microsoft.AspNetCore.Identity;

namespace DemoProject.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
