using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.Entities;
using AutoMapper;

namespace AgdtTestTask.Medical.BusinessLogic.Mapping
{
    internal class NameProfile : Profile
    {
        public NameProfile()
        {
            CreateMap<NameDTO, Name>()
                .ForMember(
                    dst => dst.Family,
                    opt => opt.PreCondition(
                        src => src.Family != null))
                .ForMember(
                    dst => dst.Id,
                    opt => opt.PreCondition(
                        src => src.Id.HasValue))
                .ForMember(
                    dst => dst.Use,
                    opt => opt.PreCondition(
                        src => src.Use.HasValue))
                .ForMember(
                    dst => dst.Given,
                    opt =>
                    {
                        opt.PreCondition(
                            src => src.Given != null);
                        opt.MapFrom(
                            src => src.Given.Select(
                                x => new GivenName { Value = x }));
                    });

            CreateMap<Name, NameDTO>()
                .ForMember(
                    dst => dst.Given,
                    opt => opt.MapFrom(
                        src => src.Given.Select(x => x.Value).ToList()));
        }
    }
}
