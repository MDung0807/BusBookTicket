using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.RoutesManage.DTOs.Requests;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Paging;

namespace BusBookTicket.RoutesManage.Service;

public interface IRoutesService : IService<RoutesCreate, RoutesCreate, int, RoutesResponse, RoutesPaging, RoutesPagingResult>
{
    
}