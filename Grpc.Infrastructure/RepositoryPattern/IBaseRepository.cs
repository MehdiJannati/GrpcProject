using Microsoft.EntityFrameworkCore.Query;
using Model;
using System.Linq.Expressions;

namespace Infrastructure.RepositoryPattern
{
    public interface IBaseRepository<T, KeyTypeId> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true);
        Task<IQueryable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true);
        Task<T> GetByIdAsync(KeyTypeId id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateDataAsync(T data);
        Task<T> UpdateDataAsync(T data);
        Task<bool> DeleteDataAsync(T data);
        Task<int> CommitAsync();
    }
}
