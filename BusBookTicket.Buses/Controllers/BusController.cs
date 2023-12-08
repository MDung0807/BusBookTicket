using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.Buses.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/buses")]
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
        var validator = new FormCreateBusValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = userId;
        await _busService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Update([FromBody] FormUpdateBus request)
    {
        var validator = new FormUpdateBusValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = userId;
        await _busService.Update(request, request.Id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("addBusStops")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> AddBusStops([FromBody] FormAddBusStop request)
    {
        var validator = new FormAddBusStopValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        BusResponse response = await _busService.AddBusStops(request, userId);
        return Ok(new Response<BusResponse>(false, response));
    }

    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        BusResponse response = await _busService.GetById(id);
        return Ok(new Response<BusResponse>(false, response));
    }

    [HttpGet("getAll")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> GetAll([FromQuery] BusPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        BusPagingResult responses = await _busService.GetAll(paging, userId);
        return Ok(new Response<BusPagingResult>(false, responses));
    }

    [HttpGet("changeIsDisable")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> ChangeIsDisable([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.ChangeToDisable(id, userId);
        return Ok(new Response<string>(!status, "AppConstants"));
    }
    
    [HttpGet("changeIsActive")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> ChangeIsActive([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.ChangeIsActive(id, userId);
        return Ok(new Response<string>(!status, "AppConstants"));
    }

    #endregion -- Controller --
}