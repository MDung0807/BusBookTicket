namespace BusBookTicket.Core.Application.Paging
{
    public class PagingRequest
    {
        private int _pageSize = 10;
        private int _pageIndex = 1;
        public int PageIndex 
        {
            get => _pageIndex;
            set => _pageIndex = Math.Max(value, _pageIndex);
        }
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = Math.Max(value, _pageSize);
        }
    }
}
