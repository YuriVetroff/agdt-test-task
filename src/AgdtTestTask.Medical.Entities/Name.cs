using AgdtTestTask.Core.Common.Enums;

namespace AgdtTestTask.Medical.Entities
{
    public class Name
    {
        public Guid? Id { get; set; }

        public NameUse Use { get; set; }

        public string Family { get; set; }

        public ICollection<GivenName> Given { get; set; }
    }
}
