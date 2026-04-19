using AgdtTestTask.Core.Common.Enums;

namespace AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO
{
    public class NameDTO
    {
        public Guid? Id { get; set; }
        public NameUse? Use { get; set; }
        public string Family { get; set; }
        public ICollection<string> Given { get; set; }
    }
}
