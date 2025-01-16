using AutoMapper;
using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.Extensions;

namespace StoronnimV.Application.Mapping.Music;

public class MusicPlatformMappingProfile : Profile
{
    public MusicPlatformMappingProfile()
    {
        CreateMap<object, MusicResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (long)src.GetPropertyValue("Id")!))
            .ForMember(dest => dest.BgImageUrl, opt => opt.MapFrom(src => (string)src.GetPropertyValue("BgImageUrl")!))
            .ForMember(dest => dest.PlatformUrl, opt => opt.MapFrom(src => (string)src.GetPropertyValue("PlatformUrl")!));
    }
}