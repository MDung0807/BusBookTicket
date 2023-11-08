using BusBookTicket.AddressManagement.DTOs.Requests.Ward;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.AddressManagement.Services.WardService;

public interface IWardService : IService<WardCreate, WardUpdate, int, WardResponse>
{
    
}