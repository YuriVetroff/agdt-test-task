using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.Entities;
using AutoMapper;

namespace AgdtTestTask.Medical.BusinessLogic.Mapping
{
    internal class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientDTO, Patient>()
                .ForMember(
                    dst => dst.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dst => dst.Name,
                    opt => opt.PreCondition(
                        src => src.Name != null))
                .ForMember(
                    dst => dst.Gender,
                    opt => opt.PreCondition(
                        src => src.Gender.HasValue))
                .ForMember(
                    dst => dst.Birthdate,
                    opt => opt.PreCondition(
                        src => src.Birthdate.HasValue))
                .ForMember(
                    dst => dst.Active,
                    opt => opt.PreCondition(
                        src => src.Active.HasValue));

            CreateMap<Patient, PatientDTO>();
        }
    }
}
