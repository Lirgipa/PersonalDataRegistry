using PersonalDataDirectory.Infrastructure.Data;

namespace PersonalDataDirectory.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task TryCommitAsync(Func<Task> action)
    {
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;
    
        if (hasActiveTransaction)
        {
            await action();
            return;
        }
    
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TResult> TryCommitAsync<TResult>(Func<Task<TResult>> action)
    {
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;

        if (!hasActiveTransaction)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await action();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        return await action();
    }
}