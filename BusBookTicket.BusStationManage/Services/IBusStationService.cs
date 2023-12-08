using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.BusStationManage.Services;

public interface IBusStationService : IService<BST_FormCreate, BST_FormUpdate, int, BusStationResponse, StationPaging, StationPagingResult>
{
    Task<BusStationResponse> GetStationByName(string name);
    Task<StationPagingResult> GetStationByLocation(string location, StationPaging pagingRequest);
    Task<StationPagingResult> GetAllStationInBus(int busId, StationPaging pagingRequest);
}