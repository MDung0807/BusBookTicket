using BusBookTicket.AddressManagement.DTOs.Requests.Unit;
using BusBookTicket.AddressManagement.DTOs.Responses.Unit;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.AddressManagement.Services.UnitService;

public interface IUnitService : IService<UnitCreate, UnitUpdate, int, UnitResponse>
{
    
}