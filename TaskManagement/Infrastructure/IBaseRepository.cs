using System.Linq.Expressions;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Services
{
    public interface IBaseRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(QueryParameters queryParams, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}