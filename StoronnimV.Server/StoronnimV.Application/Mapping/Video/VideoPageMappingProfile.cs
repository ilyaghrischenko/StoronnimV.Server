using AutoMapper;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Extensions;

namespace StoronnimV.Application.Mapping.Video;

/// <summary>
/// Профиль маппинга для мапа с (object) в (VideoPageResponse)
/// </summary>
public class VideoPageMappingProfile : Profile
{
    public VideoPageMappingProfile()
    {
        CreateMap<object, VideoPageResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (long)src.GetPropertyValue("Id")!))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Title")!))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => (string)src.GetPropertyValue("Url")!));
    }
}