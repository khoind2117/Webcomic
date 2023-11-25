namespace Webcomic.Helpers.Pagination
{
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public List<T>? Items { get; set; }
    }
}
