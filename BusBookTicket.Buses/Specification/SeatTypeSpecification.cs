using BusBookTicket.Buses.Paging.SeatType;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class SeatTypeSpecification : BaseSpecification<SeatType>
{
    public SeatTypeSpecification(int id, int userId = default, bool checkStatus = true, bool getIsChangeStatus = false) 
        : base(x => x.Id == id 
                    && (userId == default || x.CreateBy == userId),
            checkStatus: checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.Seats);
            return;
        }
        AddInclude(x => x.Company);
    }
    
    public SeatTypeSpecification(int? id = null, int? companyId = null, SeatTypePaging paging = null, int userId = default, bool checkStatus = true) 
        : base(x => (id == default || x.Id == id )
                    || x.Company.Id == companyId || x.Company.Id == default
                    || (x.IsCommon == true),
            checkStatus: checkStatus) 
    {
        AddInclude(x => x.Company);
        if (paging!= null)
            ApplyPaging(paging.PageIndex,paging.PageSize);
    }

    public void FindByParam(string param, SeatTypePaging paging = default, bool checkStatus = true)
    {
        Criteria = x => x.Type.Contains(param);
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }

        CheckStatus = checkStatus;
    }
}