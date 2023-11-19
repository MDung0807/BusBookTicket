using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class BusTypeSpecification : BaseSpecification<BusType>
{
    public BusTypeSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Buses);
    }
    
    public BusTypeSpecification(int id, bool checkStatus) : base(x => x.Id == id, checkStatus)
    {
        AddInclude(x => x.Buses);
    }

    public BusTypeSpecification()
    {
        
    }
}