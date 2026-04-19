using AgdtTestTask.Core.Entities.Interfaces;

namespace AgdtTestTask.Core.Entities
{
    public abstract class AbstractEntity : IEntity
    {
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
