using luqa_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace luqa_backend.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly LuqaContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(LuqaContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}