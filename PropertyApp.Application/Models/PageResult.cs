namespace PropertyApp.Application.Models
{
    public class PageResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalCount { get; set; }

        public PageResult(IEnumerable<T> items, int pageNumber, int totalCount, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            ItemsFrom= pageSize * (pageNumber - 1) + 1;
            ItemsTo = totalCount>pageSize ? ItemsFrom + pageSize - 1: totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }


    }
}