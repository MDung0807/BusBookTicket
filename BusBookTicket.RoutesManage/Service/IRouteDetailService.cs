using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.RoutesManage.DTOs.Requests;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Paging;

namespace BusBookTicket.RoutesManage.Service;

public interface IRouteDetailService : IService<RouteDetailCreate, RouteDetailCreate, int, RouteDetailResponse, RouteDetailPaging,RouteDetailPagingResult>
{
    
}