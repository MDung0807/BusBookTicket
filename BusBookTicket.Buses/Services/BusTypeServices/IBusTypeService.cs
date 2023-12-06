using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Buses.Paging.BusType;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public interface IBusTypeService : IService<BusTypeForm, BusTypeFormUpdate, int, BusTypeResponse, BusTypePaging, BusTypePagingResult>
{
    
}