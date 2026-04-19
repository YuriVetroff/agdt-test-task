using AgdtTestTask.Core.DataAccess.EF.Infrastructure;
using AgdtTestTask.Medical.DataAccess.EF.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AgdtTestTask.Medical.DataAccess.Migrations")]
namespace AgdtTestTask.Medical.DataAccess.EF.Infrastructure
{
    internal class AgdtMedicalDbContext
        : AbstractEFDbContext
    {
        public AgdtMedicalDbContext(
            DbContextOptions options,
            bool trackBeforeSaving = true)
            : base(options, trackBeforeSaving)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AgdtMedicalDbContext).Assembly);
        }
    }
}
