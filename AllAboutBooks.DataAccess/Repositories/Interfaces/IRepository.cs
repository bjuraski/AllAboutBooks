using System.Linq.Expressions;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetFirstOrDefaultByExpressionAsync(Expression<Func<TEntity, bool>> expression, bool shouldBeTracked = true);

    Task InsertAsync(TEntity entity);

    Task Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
}
