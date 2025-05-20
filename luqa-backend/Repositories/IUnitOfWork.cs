namespace luqa_backend.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync();
}