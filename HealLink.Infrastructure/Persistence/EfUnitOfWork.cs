using ErrorOr;
using healLink.Application.Common.Interfaces.Service;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealLink.Infrastructure.Persistence
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly HealLinkDbContext _context;
        private IDbContextTransaction? _transaction;

        public EfUnitOfWork(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task CommitChangesAsync() => await _context.SaveChangesAsync();

        public async Task StartTransactionAsync()
        {
            if (_transaction is null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await CommitChangesAsync();
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<ErrorOr<Success>> ExecuteInTransactionAsync(Func<Task> action)
        {
            await StartTransactionAsync();

            List<Error> errors = [];

            try
            {
                await action();
                await CommitTransactionAsync();

                return new Success();
            }
            catch (Exception ex)
            {
                errors.Add(Error.Failure(code: ex.Source ?? "Transaction", description: ex.Message));

                try
                {
                    await RollbackTransactionAsync();
                }
                catch (Exception rollbackEx)
                {
                    errors.Add(Error.Failure(code: rollbackEx.Source ?? "Rollback", description: rollbackEx.Message));
                }

                return ErrorOr<Success>.From(errors);
            }
        }
        public async Task<ErrorOr<Success>> ExecuteInTransactionAsync(
            params Func<Task>[] actions)
        {
            await StartTransactionAsync();
            List<Error> errors = [];

            try
            {
                foreach (var action in actions)
                {
                    await action();
                    await CommitChangesAsync(); 
                }

                await CommitTransactionAsync();
                return new Success();
            }
            catch (Exception ex)
            {
                errors.Add(Error.Failure(code: ex.Source ?? "Transaction", description: ex.Message));
                try { await RollbackTransactionAsync(); }
                catch (Exception rollbackEx)
                {
                    errors.Add(Error.Failure(code: rollbackEx.Source ?? "Rollback", description: rollbackEx.Message));
                }
                return ErrorOr<Success>.From(errors);
            }
        }


    }
}