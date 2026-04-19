using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AutoMapper;

namespace AgdtTestTask.Medical.WebApi.ViewModels.Mapping
{
    internal class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientCreatingVM, PatientDTO>();
            CreateMap<PatientUpdatingVM, PatientDTO>();

            CreateMap<PatientDTO, PatientDisplayVM>();
        }
    }
}
