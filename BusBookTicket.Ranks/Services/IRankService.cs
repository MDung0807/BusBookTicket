using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;

namespace BusBookTicket.Ranks.Services;

public interface IRankService : IService<RankCreate, RankUpdate, int, RankResponse, object, object>
{
    
}