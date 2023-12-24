using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BusStationManage.Specification;

public sealed class BusStationSpecification : BaseSpecification<BusStation>
{
    public BusStationSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.BusStops);
        AddInclude(x => x.Ward);
    }

    public BusStationSpecification(bool checkStatus = true, StationPaging paging = null) : base(null, checkStatus: checkStatus)
    {
        AddInclude(x => x.Ward);
        AddInclude(x => x.BusStops);

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
            ApplyOrderBy(x => x.DateCreate);
            ApplyOrderBy(x => x.DateUpdate);
        }    
    }

    public BusStationSpecification(string name) : base(x => x.Name.Contains(name))
    {
        AddInclude(x => x.Ward);
        AddInclude(x => x.BusStops);

    }
    
    public BusStationSpecification(string name, bool checkStatus) : base(x => x.Name.Contains(name), false)
    {
        AddInclude(x => x.Ward);
        AddInclude(x => x.BusStops);

    }
    
    public BusStationSpecification(int id, bool checkStatus, bool getIsChangeStatus = false) : base(x => x.Id == id, false)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.RouteDetails);
            AddInclude(x => x.StationStart);
            AddInclude(x => x.StationEnd);
            return;
        }
        AddInclude(x => x.Ward);
        AddInclude(x => x.BusStops);

    }

    public BusStationSpecification(string name, string location, StationPaging paging = null) : base(x =>
        x.Name == name ||
        x.Address.Contains(location) ||
        x.Ward.FullName.Contains(location) ||
        x.Ward.District.FullName.Contains(location) ||
        x.Ward.District.Province.FullName.Contains(location))
    {
        AddInclude(x => x.Ward);
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }

    public BusStationSpecification(int id, int busId, StationPaging paging = null) : base(x => x.BusStops.Any(y => y.Bus.Id == busId))
    {
        AddInclude(x => x.BusStops.Where(b => b.Bus.Id == busId));
        AddInclude(x => x.Ward);
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}