using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser defaultUser = new AppUser
            {
                FirstName = "Admin",
                LastName = "User",
                UserName = "AdminUser",
                Email = "adminuser@email.com",
                DNI = null,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(user => user.Id != defaultUser.Id))
            {
                AppUser user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pa$$word123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
    }
}