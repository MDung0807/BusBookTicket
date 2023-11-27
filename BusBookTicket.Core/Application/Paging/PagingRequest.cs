using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Application.Paging
{
    public class PagingRequest
    {
        private int _pageSize = 2;
        private int _pageIndex = 0;
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
