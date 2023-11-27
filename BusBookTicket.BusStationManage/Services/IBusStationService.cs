using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.BusStationManage.Services;

public interface IBusStationService : IService<BST_FormCreate, BST_FormUpdate, int, BusStationResponse>
{
    Task<BusStationResponse> GetStationByName(string name);
    Task<List<BusStationResponse>> GetStationByLocation(string location);
    Task<List<BusStationResponse>> GetAllStationInBus(int busId);
    Task<StationPagingResult> GetAll(StationPaging request);
}