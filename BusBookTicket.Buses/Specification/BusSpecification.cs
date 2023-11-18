using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class BusSpecification: BaseSpecification<Bus>
{
    public BusSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.BusStops);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }

    public BusSpecification()
    {
        
    }
}