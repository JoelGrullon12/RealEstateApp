using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertyViewModel, SavePropertyViewModel>
    {
        Task<List<PropertyViewModel>> GetAllViewModelFromUser();

        Task<List<PropertyViewModel>> GetAllViewModelFromClient();

        Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters);
        Task<SavePropertyViewModel> Add(SavePropertyViewModel vm);

        Task<SavePropertyViewModel> AddClient(SavePropertyViewModel vm);
        Task<PropertyViewModel> GetDetailsById(int id);
        Task<List<PropertyViewModel>> GetPropertiesOfAgent(string agentId);
        Task<List<PropertyViewModel>> GetAllWithDetails();
    }
}