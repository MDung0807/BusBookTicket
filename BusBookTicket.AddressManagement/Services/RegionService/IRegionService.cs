using BusBookTicket.AddressManagement.DTOs.Requests.Region;
using BusBookTicket.AddressManagement.DTOs.Responses.Region;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.AddressManagement.Services;

public interface IRegionService : IService<RegionCreate, RegionUpdate, int, RegionResponse, object, object>
{
    
}