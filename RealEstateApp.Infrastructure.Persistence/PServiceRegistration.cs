using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Infrastructure.Persistence.Repositories;

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

            #region Dependency Injections
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IFavoriteRepository, FavoriteRepository>();
            service.AddTransient<IPropertyImgRepository, PropertyImgRepository>();
            service.AddTransient<IPropertyRepository, PropertyRepository>();
            service.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            service.AddTransient<IPropertyUpgradeRepository, PropertyUpgradeRepository>();
            service.AddTransient<ISellTypeRepository, SellTypeRepository>();
            service.AddTransient<IUpgradeRepository, UpgradeRepository>();
            #endregion
        }
    }
}