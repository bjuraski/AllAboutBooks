using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    void SetEntityStateAsDetached(Category category);

    Task<IEnumerable<SelectListItem>> GetCategorySelectList();
}
