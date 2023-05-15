using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
