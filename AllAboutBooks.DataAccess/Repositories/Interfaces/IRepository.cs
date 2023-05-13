using System.Linq.Expressions;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression);

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    Task Save();
}
