using System.Data;

namespace AgdtTestTask.Core.DataAccess.Abstracts
{
    public interface IDatabaseFacade
    {
        Task<ITransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel);

        Task ExecuteInTransactionAsync(
            IsolationLevel isolationLevel,
            Func<Task> action);

        Task<TResult> ExecuteInTransactionAsync<TResult>(
            IsolationLevel isolationLevel,
            Func<Task<TResult>> action);
    }
}
