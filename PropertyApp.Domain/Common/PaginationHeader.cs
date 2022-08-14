namespace PropertyApp.Domain.Common;

public class PaginationHeader
{
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages, int itemFrom, int itemTo)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
        ItemFrom = itemFrom;
        ItemTo = itemTo;
    }
    public int ItemFrom { get; set; }
    public int ItemTo { get; set; }
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

