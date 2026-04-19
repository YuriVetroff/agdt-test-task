using AgdtTestTask.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgdtTestTask.Core.DataAccess.EF.EntityConfig
{
    public abstract class IdentifiableEntityConfig<T>
        : AbstractEntityConfig<T>
        where T : class, IIdentifiableEntity
    {
        public override void Configure(
            EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            base.Configure(builder);
        }
    }
}
