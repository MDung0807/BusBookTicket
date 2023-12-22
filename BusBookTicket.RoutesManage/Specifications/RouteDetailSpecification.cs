using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.RoutesManage.Paging;
using FluentValidation;

namespace BusBookTicket.RoutesManage.Specifications;

public sealed class RouteDetailSpecification : BaseSpecification<RouteDetail>
{
    public RouteDetailSpecification(int routeId = default, int companyId = default, bool checkStatus = true,
        bool isGetChangeStatus = false, RouteDetailPaging paging = null)
        : base(x => (routeId == default || x.Routes.Id == routeId)
        && (companyId == default || x.Company.Id == companyId), checkStatus)
    {
        if (isGetChangeStatus)
        {
            return;
        }

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.Routes);
        AddInclude(x => x.Station);
    }
}