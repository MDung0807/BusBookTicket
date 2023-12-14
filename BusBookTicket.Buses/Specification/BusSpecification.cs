using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Specification;

public sealed class BusSpecification : BaseSpecification<Bus>
{
    public BusSpecification(int id = default, bool checkStatus = true, bool getChangeStatus = false, int idMaster = default) 
        : base(x =>(id == default||  x.Id == id) && (idMaster ==default || x.Company.Id == idMaster), checkStatus: checkStatus)
    {
        if (getChangeStatus)
        {   
            AddInclude("BusStops.TicketBusStops");
            AddInclude("BusStops.TicketBusStops.Ticket");
            AddInclude(x => x.Seats);
            AddInclude(x => x.BusStops);
            return;
        }

        if (idMaster != default)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.BusType);
            return;
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusStops);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }


    public BusSpecification(int id, int idCompany, bool checkStatus = true, bool getIsChangeStatus = false) 
        : base(x => x.Id == id 
                    && x.Company.Id == idCompany,
            checkStatus: checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.BusStops);
            AddInclude("BusStops.TicketBusStops", tbs => tbs.Where(tb => tb.BusStops.Any(p => p.TicketBusStops.Any(p2 => p2.DepartureTime > DateTime.Now))));
            AddInclude("BusStops.TicketBusStops.Ticket");
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusStops);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }

    public BusSpecification(bool checkStatus = true): base(checkStatus:checkStatus)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
    }
}