namespace StoronnimV.Application.Models;

public class PaginationResult(int currentPage, int totalPages, int totalItems, IEnumerable<object> items)
{
    public int CurrentPage { get; set; } = currentPage;
    public int TotalPages { get; set; } = totalPages;
    public int TotalItems { get; set; } = totalItems;
    public IEnumerable<object> Items { get; set; } = items;
}