using AgdtTestTask.Core.DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgdtTestTask.Core.DataAccess.EF.Infrastructure
{
    internal sealed class EFTransaction
        : ITransaction
    {
        private readonly IDbContextTransaction _transaction;
        private readonly DbContext _context;

        public EFTransaction(
            IDbContextTransaction transaction,
            DbContext context)
        {
            _transaction = transaction;
            _context = context;
        }

        public Guid TransactionId =>
            _transaction.TransactionId;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public Task RollbackAsync()
        {
            return _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _transaction.DisposeAsync();
        }
    }
}
