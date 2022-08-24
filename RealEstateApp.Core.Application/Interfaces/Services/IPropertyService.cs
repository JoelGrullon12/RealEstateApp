using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertyViewModel, SavePropertyViewModel>
    {
<<<<<<< HEAD
        Task<List<PropertyViewModel>> GetAllViewModelFromUser();
=======
       Task<List<PropertyViewModel>> GetProperties();

>>>>>>> 92f8e3d2ea8d662396ec1dbebb4e4cff2693605d
    }
}