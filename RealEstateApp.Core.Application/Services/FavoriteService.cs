using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class FavoriteService: GenericService<Favorite, FavoriteViewModel, SaveFavoriteViewModel>, IFavoriteService
    {
        private readonly IFavoriteRepository _favRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository repo, IMapper mapper):base(repo,mapper)
        {
            _favRepository = repo;
            _mapper = mapper;
        }

        public async Task DeleteByPropAndUserId(int propId, string userId)
        {
            var favs = await _favRepository.GetAllAsync();
            var fav = favs.FirstOrDefault(f => f.PropertyId == propId && f.UserId == userId);

            if (fav != null)
            {
                await _favRepository.DeleteAsync(fav);
            }
        }
    }
}