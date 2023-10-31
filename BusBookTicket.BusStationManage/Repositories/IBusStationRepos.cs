using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BusStationManage.Repositories;

public interface IBusStationRepos : IRepository<BusStation, int>
{
    Task<BusStation> getStationByName(string name);
    Task<List<BusStation>> getAllStationByLocation(string location);
}