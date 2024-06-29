using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Buses.Services.BusServices;
using BusBookTicket.Buses.Utils;
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

    [HttpGet("find")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> Find([FromQuery] string param, [FromQuery] BusPaging paging)
    {
        var result = await _busService.FindByParam(param, paging);
        return Ok(new Response<BusPagingResult>(false, result));
    }

    [HttpGet("changeIsDisable")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> ChangeIsDisable([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.ChangeToDisable(id, userId);
        return Ok(new Response<string>(!status, BusConstants.CHANGE_SUCCESS));
    }
    
    [HttpGet("changeIsActive")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> ChangeIsActive([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.ChangeIsActive(id, userId);
        return Ok(new Response<string>(!status, BusConstants.CHANGE_SUCCESS));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> DeleteBus([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.Delete(id, userId);
        return Ok(new Response<string>(!status, "AppConstants"));
    }
    
    [HttpPost("registerRoute")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> RegisterRoute([FromQuery] int id,[FromQuery] int routeId)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _busService.RegisRoute(id, routeId, userId);
        return Ok(new Response<string>(!status, "AppConstants"));
    }

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("GetInRoute")]
    public async Task<IActionResult> GetInRoute([FromQuery] BusPaging paging, [FromQuery] int routeId)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _busService.GetInRoute(paging, routeId: routeId, companyId: userId);
        return Ok(new Response<BusPagingResult>(false, result));
    }
    #endregion -- Controller --
}