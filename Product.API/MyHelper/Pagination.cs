namespace Product.API.MyHelper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageSize, int pageNumber, int pageCount, IReadOnlyList<T>data)
        {
            PageSize = pageSize;
            PageCount = pageCount;
            PageNumber = pageNumber;
            Data = data;
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }



    }
}
