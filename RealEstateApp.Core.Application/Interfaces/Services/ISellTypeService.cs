using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.SellType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ISellTypeService : IGenericService<SellTypeViewModel, SaveSellTypeViewModel>
    {
        
    }
}