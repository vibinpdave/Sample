using HRMS.Domain.Common;

namespace HRMS.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
    }
}
