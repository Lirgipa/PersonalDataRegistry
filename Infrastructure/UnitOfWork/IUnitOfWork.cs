namespace PersonalDataDirectory.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task TryCommitAsync(Func<Task> action);
    Task<TResult> TryCommitAsync<TResult>(Func<Task<TResult>> action);
}