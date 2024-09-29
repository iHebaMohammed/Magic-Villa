namespace Demo.API.Helper
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int? pageIndex, int? pageSize, int count, IReadOnlyList<T> model)
        {
            PageIndex = pageIndex.Value;
            PageSize = pageSize.Value;
            PageCount = count;
            Data = model;
        }

    }
}
