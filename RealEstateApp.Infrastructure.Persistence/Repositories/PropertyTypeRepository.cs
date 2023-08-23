using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyTypeRepository : GenericRepository<PropertyType>, IPropertyTypeRepository
    {
        private readonly RealEstateContext _dbContext;

        public PropertyTypeRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}