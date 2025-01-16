using AutoMapper;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.Extensions;

namespace StoronnimV.Application.Mapping.Home;

public class HomeScheduleMappingProfile : Profile
{
    public HomeScheduleMappingProfile()
    {
        CreateMap<object, ScheduleHomeResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (long)src.GetPropertyValue("Id")!))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Photo")!))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Title")!))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Description")!))
            .ForMember(dest => dest.PerformanceDateTime, opt => opt.MapFrom(src => (string)src.GetPropertyValue("PerformanceDateTime")!))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Location")!));
    }
}