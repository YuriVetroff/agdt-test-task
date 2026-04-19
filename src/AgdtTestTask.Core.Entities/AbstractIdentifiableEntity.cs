using AgdtTestTask.Core.Entities.Interfaces;

namespace AgdtTestTask.Core.Entities
{
    public abstract class AbstractIdentifiableEntity
        : AbstractEntity, IIdentifiableEntity
    {
        public long Id { get; set; }
    }
}
