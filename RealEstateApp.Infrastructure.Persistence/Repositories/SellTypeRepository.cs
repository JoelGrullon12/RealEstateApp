using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class SellTypeRepository : GenericRepository<SellType>, ISellTypeRepository
    {
        private readonly RealEstateContext _dbContext;

        public SellTypeRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}