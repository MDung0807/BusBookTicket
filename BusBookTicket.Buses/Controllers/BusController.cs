using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.Core.Common;

namespace BusBookTicket.Buses.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/bus")]
public class BusController : ControllerBase
{
    private readonly IBusService _busService;

    public BusController(IBusService busService)
    {
        this._busService = busService;
    }

    #region -- Controller --

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Create([FromBody] FormCreateBus request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = userId;
        await _busService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Update([FromBody] FormUpdateBus request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = userId;
        await _busService.Update(request, request.Id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controller --
}