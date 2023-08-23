using RealEstateApp.Core.Application.ViewModels.Favorite;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IFavoriteService: IGenericService<FavoriteViewModel, SaveFavoriteViewModel>
    {
        Task DeleteByPropAndUserId(int propId, string userId);
    }
}