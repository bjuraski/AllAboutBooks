namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }

    IProductRepository ProductRepository { get; }

    Task Save();
}
