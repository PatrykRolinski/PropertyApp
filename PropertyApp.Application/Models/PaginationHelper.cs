namespace PropertyApp.Application.Models
{
    public class PaginationHelper<T>
    {
        public List<T>? Items { get; set; }
        public int totalCount { get; set; }

    }
}