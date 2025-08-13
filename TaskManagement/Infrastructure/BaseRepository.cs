using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Models;

namespace TaskManagement.Services
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<PagedResult<T>> GetAllAsync(QueryParameters queryParams, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            query = query.ApplySearch(queryParams.SearchFilters)
             .ApplySort(queryParams.SortOrders);

            var total = await query.CountAsync();

            query = query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize);

            foreach (var include in includes)
                query = query.Include(include);

            var items = await query.ToListAsync();
            return new PagedResult<T>
            {
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize,
                Items = items,
                TotalCount = total
            };
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            return entity ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}