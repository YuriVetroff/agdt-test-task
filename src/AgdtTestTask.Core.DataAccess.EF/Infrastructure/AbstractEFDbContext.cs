using AgdtTestTask.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AgdtTestTask.Core.DataAccess.EF.Infrastructure
{
    public abstract class AbstractEFDbContext : DbContext
    {
        private readonly bool _trackBeforeSaving;

        protected AbstractEFDbContext(
            DbContextOptions options,
            bool trackBeforeSaving = true)
            : base(options)
        {
            _trackBeforeSaving = trackBeforeSaving;
        }

        public override int SaveChanges()
        {
            TrackEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            TrackEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void TrackEntities()
        {
            if (!_trackBeforeSaving)
            {
                return;
            }

            foreach (var entry in GetEntries<IUpdateable>(
                EntityState.Modified)
                .Select(x => x.Entity))
            {
                entry.UpdatedAt = DateTime.UtcNow;
            }

            foreach (var entry in GetEntries<ICreateable>(
                EntityState.Added)
                .Select(x => x.Entity))
            {
                entry.CreatedAt = DateTime.UtcNow;
            }
        }

        private IEnumerable<EntityEntry<TEntity>> GetEntries<TEntity>(
            params EntityState[] entityStates)
            where TEntity : class
        {
            return ChangeTracker.Entries<TEntity>()
                .Where(entry => entityStates.Contains(
                    entry.State));
        }
    }
}
