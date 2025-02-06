using Microsoft.EntityFrameworkCore;
using PersonalDataDirectory.Infrastructure.Data;

namespace PersonalDataDirectory.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public void Delete(T entity) => _dbSet.Remove(entity);
}