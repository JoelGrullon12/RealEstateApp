using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel>
        where ViewModel : class
        where SaveViewModel : class
    {
        Task<ViewModel> Add(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Update(SaveViewModel saveViewModel, int id);
    }
}