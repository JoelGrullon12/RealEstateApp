using RealEstateApp.Core.Application.ViewModels.PropertyType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : IGenericService<PropertyTypeViewModel, SavePropertyTypeViewModel>
    {
        Task<List<PropertyTypeViewModel>> GetAllViewModelWithInclude();
    }
}