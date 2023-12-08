using BusBookTicket.AddressManagement.DTOs.Requests.District;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.Core.Application.Paging;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.AddressManagement.Services.DistrictService;

public interface IDistrictService : IService<DistrictCreate, DistrictUpdate, int, DistrictResponse, PagingRequest, PagingResult<DistrictResponse>>
{
}