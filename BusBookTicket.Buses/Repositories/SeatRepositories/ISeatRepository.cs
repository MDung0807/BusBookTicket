using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Repositories.SeatRepositories;

public interface ISeatRepository : IRepository<Seat, int>
{
    /// <summary>
    /// Get all Seat in Bus
    /// </summary>
    /// <param name="busID"></param>
    /// <returns></returns>
    Task<List<Seat>> getSeatInBus(int busID);
}