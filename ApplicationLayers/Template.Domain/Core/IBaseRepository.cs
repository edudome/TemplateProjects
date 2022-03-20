using System.Linq.Expressions;

namespace Template.Domain.Core
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> AddAsync(TEntity objModel);
        Task<int> AddOrUpdateAsync(TEntity objModel, Expression<Func<TEntity, bool>> predicate);
        Task<List<int>> AddRangeAsync(IEnumerable<TEntity> objModel);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>?> GetAllAsync();
        Task<int> CountAsync();
        Task UpdateAsync(TEntity? objModel);
        Task DeleteByIdAsync(object id);
        Task DeleteAsync(TEntity objModel);
        Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
