using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.RoutesManage.DTOs.Requests;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Paging;

namespace BusBookTicket.RoutesManage.Service;

public interface IRouteDetailService : IService<RouteDetailCreate, RouteDetailCreate, int, RouteDetailResponse, RouteDetailPaging,RouteDetailPagingResult>
{
    /// <summary>
    /// Get all item in master
    /// </summary>
    /// <param name="pagingRequest"></param>
    /// <param name="idMaster">id master</param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<RouteDetailPagingResult> GetAllInRoute(RouteDetailPaging pagingRequest, int idMaster, int userId);
}