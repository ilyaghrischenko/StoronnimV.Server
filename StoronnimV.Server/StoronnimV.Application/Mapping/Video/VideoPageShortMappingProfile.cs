using AutoMapper;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Mapping.Video;

/// <summary>
/// Профиль маппинга для мапа с (object) в (VideoPageShortResponse)
/// </summary>
public class VideoPageShortMappingProfile : Profile
{
    public VideoPageShortMappingProfile()
    {
        CreateMap<VideoShortProjection, VideoPageShortResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
    }
}