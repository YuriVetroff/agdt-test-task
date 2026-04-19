using System.ComponentModel.DataAnnotations;

namespace AgdtTestTask.Medical.WebApi.ViewModels
{
    public class NameCreatingVM
        : NameBaseVM
    {
        [Required]
        public override string Family { get; set; }
    }
}
