using Microsoft.AspNetCore.Identity;

namespace WebApiTest.AppIdentity
{
    public class AppUser : IdentityUser
    {
        public string? PostAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }
    }
}
