using AgdtTestTask.Core.Common.Enums;

namespace AgdtTestTask.Medical.WebApi.ViewModels
{
    public abstract class PatientBaseVM<T>
        where T: NameBaseVM
    {
        public virtual T Name { get; set; }

        public virtual Gender? Gender { get; set; }

        public virtual DateTime? Birthdate { get; set; }

        public virtual bool? Active { get; set; }
    }
}
