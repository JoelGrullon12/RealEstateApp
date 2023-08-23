using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyUpgradeService : IGenericService<PropertyUpgradeViewModel, SavePropertyUpgradeViewModel>
    {
        Task<List<PropertyUpgradeViewModel>> GetPropertyUpgrades(int id);
        Task DeleteByPropAndUpgrade(int propId, int upId);
        Task DeleteUpgradesByPropertyId(int propId);
    }
}