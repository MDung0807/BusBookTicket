using BusBookTicket.Buses.Paging.Bus;
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


    public BusSpecification(int id, int idCompany, bool checkStatus = true, bool getIsChangeStatus = false, DateTime dateTime = default) 
        : base(x => x.Id == id 
                    && x.Company.Id == idCompany,
            checkStatus: checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.BusStops);
            if (dateTime != default)
            {
                Criteria = x => x.BusStops.Any(bs => bs.TicketBusStops.Any(tbs => (tbs.DepartureTime >= dateTime))); 
                AddInclude("BusStops.TicketBusStops.Ticket");
            }
            return;
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
    
    public BusSpecification(int companyId, int routeId, bool checkStatus = true, bool getIsChange = false, BusPaging paging = null)
        : base( x => x.StopStations.Any(p => p.Route.Id == routeId)
    && x.Company.Id == companyId,checkStatus:checkStatus)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
    }
}