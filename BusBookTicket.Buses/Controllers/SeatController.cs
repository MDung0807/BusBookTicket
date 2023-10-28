using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/seat/")]
public class SeatController : ControllerBase
{
    #region -- Properties --

    private readonly ISeatService _seatService;
    #endregion -- Properties --

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet("getAll")]
    [AllowAnonymous]
    public async Task<IActionResult> getAllInBus([FromQuery] int busID)
    {
        List<SeatResponse> responses = await _seatService.getSeatInBus(busID);
        return Ok(new Response<List<SeatResponse>>(false, responses));
    }

    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> getByID([FromQuery] int id)
    {
        SeatResponse response = await _seatService.getByID(id);
        return Ok(new Response<SeatResponse>(false, response));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> create([FromBody] SeatForm request)
    {
        request.seatID = 0;
        await _seatService.create(request);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> delete([FromQuery] int id)
    {
        await _seatService.delete(id);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> update([FromBody] SeatForm request)
    {
        await _seatService.update(request, request.seatID);
        return Ok(new Response<string>(false, "Response"));
    }
}