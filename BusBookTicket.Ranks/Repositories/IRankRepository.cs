using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;

namespace BusBookTicket.Ranks.Repositories;

public interface IRankRepository : IRepository<Rank, int>
{
}