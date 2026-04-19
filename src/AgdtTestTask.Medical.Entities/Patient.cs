using AgdtTestTask.Core.Common.Enums;
using AgdtTestTask.Core.Entities;

namespace AgdtTestTask.Medical.Entities
{
    public class Patient
        : AbstractIdentifiableEntity
    {
        public Name Name { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public bool Active { get; set; }
    }
}
