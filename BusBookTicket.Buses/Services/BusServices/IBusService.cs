using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.BusServices;

public interface IBusService : IService<FormCreateBus, FormUpdateBus, int, BusResponse, BusPaging, BusPagingResult>
{
    Task<BusResponse> AddBusStops(FormAddBusStop request, int userId);
}