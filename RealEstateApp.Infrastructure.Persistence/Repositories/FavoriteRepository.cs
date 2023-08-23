using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly RealEstateContext _dbContext;

        public FavoriteRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}