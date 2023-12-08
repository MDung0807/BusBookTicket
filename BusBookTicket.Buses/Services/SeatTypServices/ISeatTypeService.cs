using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.SeatType;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.SeatTypServices;

public interface ISeatTypeService : IService<SeatTypeFormCreate, SeatTypeFormUpdate, int, SeatTypeResponse, SeatTypePaging, SeatTypePagingResult>
{

}