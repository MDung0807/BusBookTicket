using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/seatType")]
public class SeatTypeController : ControllerBase
{
    #region -- Properties --

    private readonly ISeatTypeService _seatTypeService;

    #endregion -- Properties --

    public SeatTypeController(ISeatTypeService service)
    {
        this._seatTypeService = service;
    }

    #region -- Controllers --

    [HttpGet("getAll")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> getAll([FromQuery] int companyID)
    {
        List<SeatTypeResponse> responses = await _seatTypeService.getAll(companyID);
        return Ok(new Response<List<SeatTypeResponse>>(false, responses));
    }

    [HttpGet("getByID")]
    [AllowAnonymous]
    public async Task<IActionResult> getByID([FromQuery] int id)
    {
        SeatTypeResponse response = await _seatTypeService.getByID(id);
        return Ok(new Response<SeatTypeResponse>(false, response));
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> create([FromBody] SeatTypeFormCreate request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        request.companyID = id;
        await _seatTypeService.create(request);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> delete([FromQuery] int id)
    {
        await _seatTypeService.delete(id);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> update([FromBody] SeatTypeFormUpdate request)
    {
        await _seatTypeService.update(request, request.typeID);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controllers --
}