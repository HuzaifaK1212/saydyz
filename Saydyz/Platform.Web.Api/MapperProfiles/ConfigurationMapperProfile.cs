using AutoMapper;
using Platform.Data.Model.Configuration;
using Platform.Web.Api.DTO;

namespace Platform.Web.Api.MapperProfiles
{
    public class ConfigurationMapperProfile : Profile
    {
        public ConfigurationMapperProfile()
        {
            CreateMap<ConfigurationProfile, ConfigurationProfileDto>()
               .ForMember(x => x.Parameters, opt => opt.MapFrom(s => s.Parameters))
               .ReverseMap();

            CreateMap<ConfigurationParameter, ConfigurationParameterDto>()
                .ReverseMap();
        }
    }
}
