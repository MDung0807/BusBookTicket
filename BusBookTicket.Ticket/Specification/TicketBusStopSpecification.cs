using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Ticket.Specification;

public sealed class TicketBusStopSpecification : BaseSpecification<Ticket_BusStop>
{
    public TicketBusStopSpecification(DateTime dateTime, string stationStart, string stationEnd)
    :base(x => x.ArrivalTime >= dateTime && x.BusStop.BusStation.Name.Contains(stationStart))
    {
        
    }

    public TicketBusStopSpecification(int ticketId) : base(x => x.Ticket.Id == ticketId)
    {
        AddInclude(x => x.BusStop.BusStation);
        AddInclude(x => x.BusStop.BusStation.Ward);
    }
    
    public TicketBusStopSpecification(int id, string action) : base(x => x.Id == id)
    {
        AddInclude(x => x.BusStop.BusStation);
        AddInclude(x => x.BusStop.BusStation.Ward);
        AddInclude(x => x.Ticket.Bus.Company);
    }
}