namespace LearnCraft.Application.Common.Models;

public record PaginationParams(int PageNumber = 1, int PageSize = 10);

public class PagedResult<T>
{
    public List<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public static PagedResult<T> Create(List<T> items, int count, int pageNumber, int pageSize)
    {
        return new PagedResult<T>(items, count, pageNumber, pageSize);
    }
}
