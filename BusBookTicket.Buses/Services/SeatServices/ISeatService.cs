using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Seat;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.SeatServices;

public interface ISeatService : IService<SeatForm, SeatForm, int ,SeatResponse, SeatPaging, SeatPagingResult>
{

}