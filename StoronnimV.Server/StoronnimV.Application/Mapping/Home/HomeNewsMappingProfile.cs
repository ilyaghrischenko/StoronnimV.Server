using AutoMapper;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.Extensions;

namespace StoronnimV.Application.Mapping.Home;

public class HomeNewsMappingProfile : Profile
{
    public HomeNewsMappingProfile()
    {
        CreateMap<object, NewsHomeResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (long)src.GetPropertyValue("Id")!))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Title")!))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Photo")!));
    }
}