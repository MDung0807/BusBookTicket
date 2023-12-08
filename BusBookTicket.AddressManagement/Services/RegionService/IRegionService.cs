using BusBookTicket.AddressManagement.DTOs.Requests.Region;
using BusBookTicket.AddressManagement.DTOs.Responses.Region;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.AddressManagement.Services;

public interface IRegionService : IService<RegionCreate, RegionUpdate, int, RegionResponse, object, object>
{
    
}