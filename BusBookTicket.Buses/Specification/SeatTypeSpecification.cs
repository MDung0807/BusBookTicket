using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class SeatTypeSpecification : BaseSpecification<SeatType>
{
    public SeatTypeSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Company);
    }
    
    public SeatTypeSpecification(int id, int companyId) 
        : base(x => x.Id == id || x.Company.Id == companyId || x.Company.Id == 0)
    {
        AddInclude(x => x.Company);
    }
}