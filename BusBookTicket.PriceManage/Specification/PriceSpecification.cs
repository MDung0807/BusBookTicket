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

    public void FindByParam(string param, PricePaging paging = default, bool checkStatus = true)
    {
        Criteria = x =>
            x.Company.Name.Contains(param) ||
            x.Routes.BusStationStart.Name.Contains(param) || x.Routes.BusStationEnd.Name.Contains(param) ||
            x.Id.ToString() == param;

        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
            
        }

        CheckStatus = checkStatus;

    }
}