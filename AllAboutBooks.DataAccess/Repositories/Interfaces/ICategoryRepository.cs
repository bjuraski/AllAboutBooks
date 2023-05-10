using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);

    Task Save();

    void SetEntityStateAsDetached(Category category);
}
