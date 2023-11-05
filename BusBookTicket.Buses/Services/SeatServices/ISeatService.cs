using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.SeatServices;

public interface ISeatService : IService<SeatForm, SeatForm, int ,SeatResponse>
{
    Task<List<SeatResponse>> getSeatInBus(int busID);

}