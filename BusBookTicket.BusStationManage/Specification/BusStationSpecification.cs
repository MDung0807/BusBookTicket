using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.Core.Application.Paging;
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

    public BusStationSpecification(bool checkStatus = true, bool isPaging = true, StationPaging paging = null!) : base(null, checkStatus: checkStatus)
    {
        AddInclude(x => x.Ward);
        if (!isPaging)
            return;
        ApplyPaging(paging.PageIndex, paging.PageSize);
    }

    public BusStationSpecification(string name) : base(x => x.Name == name)
    {
    }
    
    public BusStationSpecification(string name, bool checkStatus) : base(x => x.Name == name, false)
    {
    }
    
    public BusStationSpecification(int id, bool checkStatus) : base(x => x.Id == id, false)
    {
    }

    public BusStationSpecification(string name, string location) : base(x =>
        x.Name == name || x.Address.Contains(location)){}

    public BusStationSpecification(int id, int busId) : base(x => x.BusStops.Any(y => y.Bus.Id == busId))
    {
        AddInclude(x => x.BusStops.Where(b => b.Bus.Id == busId));
    }
}