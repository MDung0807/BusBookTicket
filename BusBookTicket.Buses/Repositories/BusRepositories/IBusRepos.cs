using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Repositories.BusTypeRepositories;

public interface IBusRepos : IRepository<Bus, int>
{
    Task<int> createStopStation(BusStop busStop);
}