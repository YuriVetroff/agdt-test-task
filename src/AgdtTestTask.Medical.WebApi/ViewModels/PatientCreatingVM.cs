using AgdtTestTask.Core.Web.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgdtTestTask.Medical.WebApi.ViewModels
{
    public class PatientCreatingVM
        : PatientBaseVM<NameCreatingVM>
    {
        [Required]
        public override NameCreatingVM Name { get; set; }

        [Required]
        [Birthdate]
        public override DateTime? Birthdate { get; set; }
    }
}
