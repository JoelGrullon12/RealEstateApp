using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class UpgradeRepository : GenericRepository<Upgrade>, IUpgradeRepository
    {
        private readonly RealEstateContext _dbContext;

        public UpgradeRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}