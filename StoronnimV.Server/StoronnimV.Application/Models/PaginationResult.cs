namespace StoronnimV.Application.Models;

public class PaginationResult(int currentPage, int totalPages, int totalItems, IEnumerable<object> items)
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<object> Items { get; set; } = items;
}