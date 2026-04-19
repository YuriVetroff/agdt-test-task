using AgdtTestTask.Core.DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AgdtTestTask.Core.DataAccess.EF.Infrastructure
{
    internal sealed class EFDatabaseFacade
        : IDatabaseFacade
    {
        private readonly DbContext _context;
        private readonly ILogger _logger;

        public EFDatabaseFacade(DbContext context,
            ILogger<EFDatabaseFacade> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ITransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel)
        {
            return new EFTransaction(
                await _context.Database
                    .BeginTransactionAsync(isolationLevel),
                _context);
        }

        public async Task ExecuteInTransactionAsync(
            IsolationLevel isolationLevel,
            Func<Task> action)
        {
            var transaction = await BeginTransactionAsync(
                isolationLevel);

            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                LogError(ex, transaction);

                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TResult> ExecuteInTransactionAsync<TResult>(
            IsolationLevel isolationLevel,
            Func<Task<TResult>> action)
        {
            var transaction = await BeginTransactionAsync(
                isolationLevel);

            try
            {
                var result = await action();
                await transaction.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, transaction);

                await transaction.RollbackAsync();
                throw;
            }
        }

        private void LogError(Exception ex, ITransaction transaction)
        {
            _logger.LogError(ex,
                $"Error when executing transaction {transaction.TransactionId}");
        }
    }
}
