using BusBookTicket.AddressManagement.DTOs.Requests.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.AddressManagement.Services.ProvinceService;

public interface IProvinceService : IService<ProvinceCreate, ProvinceUpdate, int, ProvinceResponse, object, object>
{
    
}