using AgdtTestTask.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgdtTestTask.Core.DataAccess.EF.EntityConfig
{
    public abstract class AbstractEntityConfig<T>
        : IEntityTypeConfiguration<T>
        where T : class, IEntity
    {
        public virtual void Configure(
            EntityTypeBuilder<T> builder)
        {
            builder
                .Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
