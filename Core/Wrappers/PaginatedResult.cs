namespace Core.Wrappers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;

        }
        public List<T> Data { get; }
        internal PaginatedResult(bool succeedd, List<T> data = default, List<string> message = null, int count = 0, int page = 1, int pagesize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeedd = succeedd;
            Pagesize = pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            TotalCount = count;



        }
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pagesize)
        {
            return new(true, data, null, count, page, pagesize);
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int TotalCount { get; set; }
        public object Meta { get; set; }
        public int Pagesize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string> Message { get; set; } = new();
        public bool Succeedd { get; set; }





    }
}
