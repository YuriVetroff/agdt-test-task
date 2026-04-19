using AgdtTestTask.Core.Common.Enums;
using AgdtTestTask.Core.Web.Attributes.Validation;

namespace AgdtTestTask.Medical.WebApi.ViewModels
{
    public class NameBaseVM
    {
        public virtual Guid? Id { get; set; }

        public virtual NameUse? Use { get; set; }

        public virtual string Family { get; set; }

        [NoStringDuplicates]
        public virtual ICollection<string> Given { get; set; }
    }
}
