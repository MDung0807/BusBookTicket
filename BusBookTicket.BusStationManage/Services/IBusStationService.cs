using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.Common.Common;

namespace BusBookTicket.BusStationManage.Services;

public interface IBusStationService : IService<BST_FormCreate, BST_FormUpdate, int, BusStationResponse>
{
    
}