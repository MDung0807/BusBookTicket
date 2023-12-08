using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public interface IBusService : IService<FormCreateBus, FormUpdateBus, int, BusResponse, BusPaging, BusPagingResult>
{
    Task<BusResponse> AddBusStops(FormAddBusStop request, int userId);
}