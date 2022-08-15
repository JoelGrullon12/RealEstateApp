using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly RealEstateContext _dbContext;

        public GenericRepository(RealEstateContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity t)
        {
            await _dbContext.Set<Entity>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public virtual async Task UpdateAsync(Entity t, int id)
        {
            Entity entry = await _dbContext.Set<Entity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(t);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity t)
        {
            _dbContext.Set<Entity>().Remove(t);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<List<Entity>> GetAllWithIncludes(List<string> props)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string prop in props)
            {
                query = query.Include(prop);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByIdWithIncludes(int id, List<string> props, List<string> colls)
        {
            var query = await _dbContext.Set<Entity>().FindAsync(id);

            foreach (string prop in props)
            {
                _dbContext.Entry(query).Reference(prop).Load();
            }

            foreach (string col in colls)
            {
                _dbContext.Entry(query).Collection(col).Load();
            }

            return query;
        }
    }
}
