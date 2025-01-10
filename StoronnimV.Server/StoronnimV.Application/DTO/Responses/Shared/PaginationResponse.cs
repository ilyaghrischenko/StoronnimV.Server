namespace StoronnimV.Application.DTO.Responses.Shared;

public class PaginationResponse<T> where T : BaseResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<T> ShortNews { get; set; } = new List<T>();
    
    public PaginationResponse() {}

    public PaginationResponse(int currentPage, int totalPages, int totalItems,
        IEnumerable<T> shortNews)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalItems = totalItems;
        ShortNews = shortNews;
    }
}