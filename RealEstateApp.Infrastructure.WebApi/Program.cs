using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealEstateApp.Infrastructure.Identity.Entities;
using RealEstateApp.Infrastructure.Identity.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var s = scope.ServiceProvider;

                try
                {
                    var userM = s.GetRequiredService<UserManager<AppUser>>();
                    var roleM = s.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedsAsync(roleM);
                    await DefaultAdmin.SeedsAsync(userM, roleM);
                    await DefaultAgent.SeedsAsync(userM, roleM);
                    await DefaultClient.SeedsAsync(userM, roleM);
                    await DefaultDeveloper.SeedsAsync(userM, roleM);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error ejecutando seeds automaticas: {ex.Message}");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
