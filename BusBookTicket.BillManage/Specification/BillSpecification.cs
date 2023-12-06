using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Specification;

public sealed class BillSpecification : BaseSpecification<Bill>
{
    public BillSpecification(int id) : base(x => x.Id == id, false)
    {
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.BusStationStart);
        AddInclude(x => x.Customer);
    }
    
    public BillSpecification(int userId, bool checkStatus = true, BillPaging paging = null) : base(x => x.Customer.Id == userId, checkStatus)
    {
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.BusStationStart);
        AddInclude(x => x.Customer);
        
        if (paging != null)
            ApplyPaging(paging.PageIndex, paging.PageSize);
    }
}