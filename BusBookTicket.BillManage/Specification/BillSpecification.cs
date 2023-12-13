using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace BusBookTicket.BillManage.Specification;

public sealed class BillSpecification : BaseSpecification<Bill>
{

    public BillSpecification(int id = default, int userId = default, bool checkStatus = true, BillPaging paging = null, int status = default, bool delete = false)
        : base(x => (userId == default || x.Customer.Id == userId) && 
                    (id == default|| x.Id == id ) &&
            (status == default || x.Status == status) &&
            (delete == true && x.Status == status ),
            checkStatus)
    {
        if (id != default)
        {
            // AddInclude(x => x.BillItems);
            return;
        }
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.BusStationEnd.BusStop.BusStation);
        AddInclude(x => x.BusStationStart.BusStop.BusStation);
        AddInclude(x => x.BusStationStart);
        AddInclude(x => x.Customer);
        AddInclude(x => x.BillItems);
        
        
        if (paging != null)
            ApplyPaging(paging.PageIndex, paging.PageSize);
    }

    public BillSpecification(int busId, int userId) : base(x =>
        x.Customer.Id == userId && x.BillItems.Any(p => p.TicketItem.Ticket.Bus.Id == busId))
    {
        
    }

    public BillSpecification(int id, bool getIsChangeStatus = false, bool checkStatus = true, DateTime dateTime = default)
        : base(x => x.Id == id  &&(dateTime == default 
                                   || x.DateDeparture >= dateTime.AddDays(3)),
            checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.BillItems);
            return;
        }
        
        AddInclude(x => x.BusStationStart.BusStop.BusStation);
        AddInclude(x => x.BusStationStart);

    }
}