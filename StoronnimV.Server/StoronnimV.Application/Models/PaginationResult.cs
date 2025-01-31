namespace StoronnimV.Application.Models;

public class PaginationResult
{
    public required int CurrentPage { get; init; }
    public required int TotalPages { get; init; }
    public required int TotalItems { get; init; }
    public required IEnumerable<object> Items { get; init; }
}