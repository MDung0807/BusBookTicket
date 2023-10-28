using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Repositories.SeatTypeRepositories;

public interface ISeatTypeRepos : IRepository<SeatType, int>
{
    Task<List<SeatType>> getAll(int idCompany);
}