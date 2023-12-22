﻿using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.RoutesManage.Paging;

namespace BusBookTicket.RoutesManage.Specifications;

public sealed class RouteSpecifications : BaseSpecification<Routes>
{
    public RouteSpecifications(int id = default, int companyId = default, bool checkStatus = true, bool getIsChangeStatus = false, RoutesPaging paging = null)
    : base(x => (id == default || x.Id == id)
        && (companyId == default || x.RouteDetails.Any(p => p.Company.Id == companyId)), checkStatus)
    {
        if (getIsChangeStatus)
        {
            return;
        }

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.BusStationStart);
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.BusStationEnd);
        AddInclude(x => x.RouteDetails);
        AddInclude("RouteDetails.Company");
        AddInclude("RouteDetails.BusStation");

    }
}