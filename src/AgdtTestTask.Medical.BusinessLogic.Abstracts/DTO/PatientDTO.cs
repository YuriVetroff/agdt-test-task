using AgdtTestTask.Core.Common.Enums;

namespace AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }

        public NameDTO Name { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public bool? Active { get; set; }
    }
}
