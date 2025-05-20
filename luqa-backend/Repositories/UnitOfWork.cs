using luqa_backend.Models;

namespace luqa_backend.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LuqaContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(LuqaContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>(); 
    }

    // método genérico para obtener todos los repositorios (entidades)
    public IRepository<T> Repository<T>() where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IRepository<T>)_repositories[typeof(T)];
        }

        var repository = new Repository<T>(_context);
        _repositories[typeof(T)] = repository;
        return repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context?.Dispose();
    }
}