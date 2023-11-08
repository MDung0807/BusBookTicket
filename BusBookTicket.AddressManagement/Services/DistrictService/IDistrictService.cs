using BusBookTicket.AddressManagement.DTOs.Requests.District;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.AddressManagement.Services.DistrictService;

public interface IDistrictService : IService<DistrictCreate, DistrictUpdate, int, DistrictResponse>
{
}