using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAgent
    {
        public static async Task SeedsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new AppUser
            {
                FirstName = "Agent",
                LastName = "User",
                UserName = "AgentUser",
                Email = "agentuser@email.com",
                DNI = null,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(user => user.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pa$$word123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agent.ToString());
                }
            }
        }
    }
}