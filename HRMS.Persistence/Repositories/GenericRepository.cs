using HRMS.Application.Contracts.Persistence;
using HRMS.Domain.Common;
using HRMS.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly HrDatabaseContext _context;

        public GenericRepository(HrDatabaseContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (include != null)
            {
                query = include(query); // Apply Include dynamically
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (include != null)
            {
                query = include(query); // Apply Include dynamically
            }

            var entity = await query.FirstOrDefaultAsync(q => q.Id == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
            }

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}
