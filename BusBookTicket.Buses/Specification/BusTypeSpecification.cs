using BusBookTicket.Buses.Paging.BusType;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class BusTypeSpecification : BaseSpecification<BusType>
{
    public BusTypeSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Buses);
    }
    
    public BusTypeSpecification(int id, bool checkStatus = true) : base(x => x.Id == id, checkStatus)
    {
        AddInclude(x => x.Buses);
    }

    public BusTypeSpecification(BusTypePaging paging = null)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}