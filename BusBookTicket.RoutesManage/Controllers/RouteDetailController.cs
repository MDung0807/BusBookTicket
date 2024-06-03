using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Utils;
using BusBookTicket.RoutesManage.DTOs.Requests;
using BusBookTicket.RoutesManage.Paging;
using BusBookTicket.RoutesManage.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.RoutesManage.Controllers;

[ApiController]
[Route("api/routedetails")]
public class RouteDetailController : ControllerBase
{
    private readonly IRouteDetailService _service;

    public RouteDetailController(IRouteDetailService service)
    {
        _service = service;
    }

    #region -- Controller --

    [HttpPost("Create")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> Create([FromBody] RouteDetailCreate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var status = await _service.Create(request, userId);
        return Ok(new Response<string>(!status, AppConstants.SUCCESS));
    }
    
    [HttpGet("getInCompany")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> GetInCompany([FromQuery]RouteDetailPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.GetAll(pagingRequest: paging, idMaster: userId);
        return Ok(new Response<RouteDetailPagingResult>(false, result));
    }
    
    [HttpGet("getInRoute")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> GetInRoute([FromQuery]RouteDetailPaging paging, [FromQuery] int routeId)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.GetAllInRoute(pagingRequest: paging, idMaster: routeId, userId: userId);
        return Ok(new Response<RouteDetailPagingResult>(false, result));
    }

    
    #endregion -- Controller --
}