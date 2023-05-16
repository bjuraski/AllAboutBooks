using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    public ApplicationUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
