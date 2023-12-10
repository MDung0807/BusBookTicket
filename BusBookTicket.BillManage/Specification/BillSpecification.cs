using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using Microsoft.AspNetCore.WebUtilities;

namespace BusBookTicket.BillManage.Specification;

public sealed class BillSpecification : BaseSpecification<Bill>
{

    public BillSpecification(int id = default, int userId = default, bool checkStatus = true, BillPaging paging = null)
        : base(x => userId == default || x.Customer.Id == userId && (id == default|| x.Id == id), checkStatus)
    {
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.BusStationStart);
        AddInclude(x => x.Customer);
        
        if (paging != null)
            ApplyPaging(paging.PageIndex, paging.PageSize);
    }

    public BillSpecification(int busId, int userId) : base(x =>
        x.Customer.Id == userId && x.BillItems.Any(p => p.TicketItem.Ticket.Bus.Id == busId))
    {
        
    }
}