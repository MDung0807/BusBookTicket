using BusBookTicket.Auth.Security;
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
    public async Task<IActionResult> GetAllInBus([FromQuery] int busID)
    {
        List<SeatResponse> responses = await _seatService.getSeatInBus(busID);
        return Ok(new Response<List<SeatResponse>>(false, responses));
    }

    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        SeatResponse response = await _seatService.GetById(id);
        return Ok(new Response<SeatResponse>(false, response));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Create([FromBody] SeatForm request)
    {
        request.seatID = 0;
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatService.Delete(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Update([FromBody] SeatForm request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatService.Update(request, request.seatID, userId);
        return Ok(new Response<string>(false, "Response"));
    }
}