using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AutoMapper;

namespace AgdtTestTask.Medical.WebApi.ViewModels.Mapping
{
    internal class NameProfile : Profile
    {
        public NameProfile()
        {
            CreateMap<NameCreatingVM, NameDTO>();
            CreateMap<NameUpdatingVM, NameDTO>()
                .ForMember(
                    dst => dst.Given,
                    opt =>
                    {
                        opt.PreCondition(
                            src => src.Given != null);
                        opt.MapFrom(
                            src => src.Given);
                    });

            CreateMap<NameDTO, NameBaseVM>();
        }
    }
}
