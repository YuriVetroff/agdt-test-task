using AgdtTestTask.Core.Common.Interfaces;

namespace AgdtTestTask.Core.Entities.Interfaces
{
    public interface IIdentifiableEntity
        : IEntity, IIdentifiable<long>
    {
    }
}
