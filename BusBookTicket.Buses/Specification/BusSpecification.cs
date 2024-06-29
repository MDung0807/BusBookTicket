using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Specification;

public sealed class BusSpecification : BaseSpecification<Bus>
{
    public BusSpecification(int id = default, bool checkStatus = true, bool getChangeStatus = false, int idMaster = default) 
        : base(x =>(id == default||  x.Id == id) && (idMaster ==default || x.Company.Id == idMaster), checkStatus: checkStatus)
    {
        if (getChangeStatus)
        {   
            AddInclude(x => x.Seats);
            return;
        }

        if (idMaster != default)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.BusType);
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
        AddInclude(x => x.StopStations.Where(s => s.Bus.Id == id));
        AddInclude("StopStations.Route.RouteDetails");
        AddInclude("StopStations.Route.RouteDetails.Station");
    }


    public BusSpecification(int id, int idCompany, bool checkStatus = true, bool getIsChangeStatus = false, DateTime dateTime = default) 
        : base(x => x.Id == id 
                    && x.Company.Id == idCompany,
            checkStatus: checkStatus)
    {
        if (getIsChangeStatus)
        {
            if (dateTime != default)
            {
            }
            return;
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }

    public BusSpecification(bool checkStatus = true): base(checkStatus:checkStatus)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
    }
    
    public BusSpecification(int companyId, int routeId, bool checkStatus = true, bool getIsChange = false, BusPaging paging = null)
        : base( x => x.StopStations.Any(p => p.Route.Id == routeId)
    && x.Company.Id == companyId,checkStatus:checkStatus)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
    }
    
    public BusSpecification(){}

    public void FindByParam(string param, BusPaging paging = default, bool checkStatus = true)
    {
        Criteria = x =>
            x.Company.Name.Contains(param) || x.StopStations.Any(s => s.Bus.BusNumber.Contains(param))
                || x.StopStations.Any(s => x.BusType.Name.Contains(param) || s.Route.BusStationStart.Name.Contains(param) || s.Route.BusStationEnd.Name.Contains(param)
                || s.Route.BusStationEnd.Ward.Name.Contains(param) || s.Route.BusStationEnd.Ward.District.Name.Contains(param) || s.Route.BusStationEnd.Ward.District.Province.Name.Contains(param));
        CheckStatus = checkStatus;

        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
        AddInclude(x => x.StopStations);
        AddInclude("StopStations.Route.RouteDetails");
        AddInclude("StopStations.Route.RouteDetails.Station");
    }
}