using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model;
using System.Linq.Expressions;

namespace Infrastructure.RepositoryPattern
{
    public class BaseRepository<T, KeyTypeId> : IBaseRepository<T, KeyTypeId> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
    {
        private readonly GrpcProjectDbContext _dbContext;
        private readonly DbSet<T> _entitySet;
        public BaseRepository(GrpcProjectDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = dbContext.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                return await Task.Run(() => isNoTracking == true ? _entitySet.AsNoTracking() : _entitySet.AsQueryable());
            }
            return await Task.Run(() => isNoTracking ? _entitySet.Where(predicate).AsNoTracking() : _entitySet.Where(predicate));
        }

        public async Task<IQueryable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true)
        {
            IQueryable<T> query = _entitySet;
            query = isNoTracking == true ? query.AsNoTracking() : query.AsQueryable();
            query = predicate != null ? query.Where(predicate) : query;
            query = include != null ? include(query) : query;
            return query != null && query.Any() ? query.Select(selector) : default;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entitySet.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(KeyTypeId id)
        {
            return await GetAsync(x => x.Equals(id));
        }

        public async Task<T> CreateDataAsync(T data)
        {
            await _entitySet.AddAsync(data);
            await CommitAsync();
            return data;
        }

        public async Task<T> UpdateDataAsync(T data)
        {
            _entitySet.Update(data);
            await CommitAsync();
            return data;
        }

        public async Task<bool> DeleteDataAsync(T data)
        {
            try
            {
                _entitySet.Remove(data);
                await CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();
    }
}
