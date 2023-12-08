using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Seat;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.Buses.Services.SeatServices;

public interface ISeatService : IService<SeatForm, SeatForm, int ,SeatResponse, SeatPaging, SeatPagingResult>
{

}