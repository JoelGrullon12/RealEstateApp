using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence
{
    public static class PServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<RealEstateContext>(
                opt => opt.UseSqlServer(
                    configuration.GetConnectionString("RealEstateConnection"),
                    migration => migration.MigrationsAssembly(typeof(RealEstateContext).Assembly.FullName)));
        }
    }
}
