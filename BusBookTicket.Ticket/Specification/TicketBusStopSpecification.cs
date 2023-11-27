using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Ticket.Specification;

public class TicketBusStopSpecification : BaseSpecification<Ticket_BusStop>
{
    public TicketBusStopSpecification(DateTime dateTime, string stationStart, string stationEnd)
    :base(x => x.ArrivalTime >= dateTime && x.BusStop.BusStation.Name.Contains(stationStart))
    {
        
    }
}