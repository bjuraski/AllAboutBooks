using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; private set; }

    public IProductRepository ProductRepository { get; private set; }

    public ICompanyRepository CompanyRepository { get; private set; }

    public IShoppingCartRepository ShoppingCartRepository { get; private set; }

    public IApplicationUserRepository ApplicationUserRepository { get; private set; }

    private readonly ApplicationDbContext _applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        CategoryRepository = new CategoryRepository(_applicationDbContext);
        ProductRepository = new ProductRepository(_applicationDbContext);
        CompanyRepository = new CompanyRepository(_applicationDbContext);
        ShoppingCartRepository = new ShoppingCartRepository(_applicationDbContext);
        ApplicationUserRepository = new ApplicationUserRepository(_applicationDbContext);
    }

    public async Task Save()
    {
        await _applicationDbContext.SaveChangesAsync();
    }
}
