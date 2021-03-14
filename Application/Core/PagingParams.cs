namespace Application.Core
{
    public class PagingParams
    {
        private int MaxPageSize { get; } = 10;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 2;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
