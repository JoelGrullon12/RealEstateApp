using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyUpgradeService : IGenericService<PropertyUpgradeViewModel, SavePropertyUpgradeViewModel>
    {
        
    }
}