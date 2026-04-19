namespace AgdtTestTask.Core.DataAccess.Abstracts
{
    public interface ITransaction
        : IDisposable, IAsyncDisposable
    {
        Guid TransactionId { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
