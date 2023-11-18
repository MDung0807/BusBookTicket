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

    public BusStationSpecification()
    {
        AddInclude(x => x.Ward);
    }

    public BusStationSpecification(string name) : base(x => x.Name == name)
    {
    }
    
    public BusStationSpecification(string name, bool checkStatus) : base(x => x.Name == name, false)
    {
    }

    public BusStationSpecification(string name, string location) : base(x =>
        x.Name == name || x.Address.Contains(location)){}
}