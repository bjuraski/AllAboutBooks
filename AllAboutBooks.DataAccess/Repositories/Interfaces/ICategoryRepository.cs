using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    void SetEntityStateAsDetached(Category category);
}
