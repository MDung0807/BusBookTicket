using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.PriceManage.Paging;

namespace BusBookTicket.PriceManage.Specification;

public sealed class PriceSpecification : BaseSpecification<Prices>
{
    public PriceSpecification(int id = default, int companyId = default, int routeId = default,
        bool checkStatus = true, bool getIsChange = false, PricePaging paging= null)
    :base(x => (id == default || x.Id == id)
    && (companyId == default || x.Company.Id == companyId)
    && (routeId == default || x.Routes.Id == routeId), checkStatus)
    {
        if (getIsChange)
        {
            AddInclude(x => x.Company);
            return;
        }

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Routes);
        AddInclude(x => x.Routes.BusStationStart);
        AddInclude(x => x.Routes.BusStationEnd);
        AddInclude(x => x.Company);
        
        ApplyOrderByDescending(x => x.DateUpdate);
    }
}