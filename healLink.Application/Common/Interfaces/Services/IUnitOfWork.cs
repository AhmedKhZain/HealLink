using ErrorOr;

namespace healLink.Application.Common.Interfaces.Service;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<ErrorOr<Success>> ExecuteInTransactionAsync(Func<Task> action);
    Task<ErrorOr<Success>> ExecuteInTransactionAsync(params Func<Task>[] actions);

}
