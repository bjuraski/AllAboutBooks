using System.Linq.Expressions;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();

    Task<T> GetByExpression(Expression<Func<T, bool>> expression);

    Task Add(T entity);

    Task Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task Save();
}
