using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.Core.Common;

namespace BusBookTicket.BusStationManage.Services;

public interface IBusStationService : IService<BST_FormCreate, BST_FormUpdate, int, BusStationResponse>
{
    
}