using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.NewsPage;

public class NewsPaginationResponse : BaseResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<NewsShortResponse> ShortNews { get; set; } = new List<NewsShortResponse>();
    
    public NewsPaginationResponse() {}

    public NewsPaginationResponse(int currentPage, int totalPages, int totalItems,
        IEnumerable<NewsShortResponse> shortNews)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalItems = totalItems;
        ShortNews = shortNews;
    }
}