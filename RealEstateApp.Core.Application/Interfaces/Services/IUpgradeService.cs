using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUpgradeService : IGenericService<UpgradeViewModel, SaveUpgradeViewModel>
    {
        
    }
}