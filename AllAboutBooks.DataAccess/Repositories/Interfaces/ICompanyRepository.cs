using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
    Task<IEnumerable<SelectListItem>> GetCompanySelectList();
}
