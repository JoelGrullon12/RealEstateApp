using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;

namespace RealEstateApp.Core.Application
{
    public static class AServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Dependency Injection
            services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,,>));
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IPropertyImgService, PropertyImgService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IPropertyUpgradeService, PropertyUpgradeService>();
            services.AddTransient<ISellTypeService, SellTypeService>();
            services.AddTransient<IUpgradeService, UpgradeService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}