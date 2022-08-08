using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyImg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyImgService : IGenericService<PropertyImgViewModel, SavePropertyImgViewModel>
    {
        
    }
}