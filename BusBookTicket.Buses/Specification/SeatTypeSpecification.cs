using BusBookTicket.Buses.Paging.SeatType;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class SeatTypeSpecification : BaseSpecification<SeatType>
{
    public SeatTypeSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Company);
    }
    
    public SeatTypeSpecification(int? id = null, int? companyId = null, SeatTypePaging paging = null) 
        : base(x => x.Id == id || x.Company.Id == companyId || x.Company.Id == 0)
    {
        AddInclude(x => x.Company);
        if (paging!= null)
            ApplyPaging(paging.PageIndex,paging.PageSize);
    }
}