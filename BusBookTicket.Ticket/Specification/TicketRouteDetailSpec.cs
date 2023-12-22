using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using Org.BouncyCastle.Asn1.Cms;

namespace BusBookTicket.Ticket.Specification;

public sealed class TicketRouteDetailSpec : BaseSpecification<Ticket_RouteDetail>
{
    public TicketRouteDetailSpec(int id = default, bool checkStatus = true)
    :base(x => (id== default || x.Id == id), checkStatus)
    {
        AddInclude(x => x.Ticket);
        AddInclude(x => x.RouteDetail);
        AddInclude(x => x.RouteDetail.Station);
        AddInclude(x => x.RouteDetail.Company);
    }
}