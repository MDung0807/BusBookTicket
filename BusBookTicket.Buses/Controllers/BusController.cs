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
    public async Task<IActionResult> create([FromBody] FormCreateBus request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        request.companyID = id;
        await _busService.create(request);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> update([FromBody] FormUpdateBus request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        request.companyID = id;
        await _busService.update(request, request.busID);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controller --
}