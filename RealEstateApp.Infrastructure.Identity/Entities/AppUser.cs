using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string ImgUrl { get; set; }
    }
}