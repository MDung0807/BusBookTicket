using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Seat;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Buses.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/seats/")]
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
    public async Task<IActionResult> GetAllInBus([FromQuery] int busId, [FromQuery] SeatPaging paging)
    {
        SeatPagingResult responses = await _seatService.GetAll(paging, busId);
        return Ok(new Response<SeatPagingResult>(false, responses));
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
        var validator = new SeatFormValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        request.SeatId = 0;
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
        var validator = new SeatFormValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatService.Update(request, request.SeatId, userId);
        return Ok(new Response<string>(false, "Response"));
    }
}