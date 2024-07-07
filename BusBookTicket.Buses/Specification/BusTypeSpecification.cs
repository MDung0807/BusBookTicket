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

    public BusTypeSpecification(BusTypePaging paging = null, bool checkStatus = false) : base(null, checkStatus:checkStatus)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }

    public void FindByParam(string param, BusTypePaging paging = default, bool checkStatus = true)
    {
        Criteria = x => x.Name.Contains(param);
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }

        CheckStatus = checkStatus;
    }

    public void Statistical(int idMaster, bool checksStatus)
    {
        CheckStatus = checksStatus;
        
        AddInclude(x => x.Buses.Where(b => b.Company.Id == idMaster));
    }
}