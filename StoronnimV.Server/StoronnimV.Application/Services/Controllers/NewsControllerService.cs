using AutoMapper;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;

namespace StoronnimV.Application.Services.Controllers;

/// <summary>
/// Сервис для маппинга данных с бд и возвращения контроллеру
/// </summary>
/// <param name="newsService"></param>
/// <param name="mapper"></param>
public class NewsControllerService(
    INewsService newsService,
    IMapper mapper) : INewsControllerService
{
    private readonly INewsService _newsService = newsService;
    private readonly IMapper _mapper = mapper;

    public async Task<NewsResponse> GetItemByIdAsync(long id)
    {
        var newsItem = await _newsService.GetItemByIdAsync(id);

        var newsItemDto = _mapper.Map<NewsResponse>(newsItem);
        return newsItemDto;
    }

    public async Task<IEnumerable<NewsResponse>> GetAllAsync()
    {
        var sortedNews = await _newsService.GetAllAsync();

        var newsDto = _mapper.Map<IEnumerable<NewsResponse>>(sortedNews);
        return newsDto;
    }

    public async Task<IEnumerable<NewsShortResponse>> GetForPageAsync(int page)
    {
        var sortedNews = await _newsService.GetForPageAsync(page);
        
        var newsDto = _mapper.Map<IEnumerable<NewsShortResponse>>(sortedNews);
        return newsDto;
    }
}