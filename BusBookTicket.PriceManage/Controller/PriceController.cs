using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Paging;
using BusBookTicket.PriceManage.Services;
using BusBookTicket.PriceManage.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.PriceManage.Controller;

[ApiController]
[Route("api/prices")]
public class PriceController: ControllerBase
{
    private readonly IPriceService _service;

    public PriceController(IPriceService service)
    {
        _service = service;
    }

    #region -- Controller --

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] PriceCreate request)
    {
        var validator = new PriceValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }

        int userId = JwtUtils.GetUserID(HttpContext);
        var status = await _service.Create(request, userId);
        return Ok(new Response<string>(!status, AppConstants.SUCCESS));
    }

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("getInCompany")]
    public async Task<IActionResult> GetInCompany([FromQuery] PricePaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.GetAll(paging, userId);
        return Ok(new Response<PricePagingResult>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("getInRoute")]
    public async Task<IActionResult> GetInRoute([FromQuery] int routeId)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.GetInRoute( routeId, userId);
        return Ok(new Response<PriceResponse>(false, result));
    }

    [Authorize(Roles = AppConstants.ADMIN)]
    [HttpPut("ChangeIsActive")]
    public async Task<IActionResult> ChangeIsActive([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.ChangeIsActive(id, userId);
        return Ok(new Response<string>(!result, AppConstants.SUCCESS));
    }
    
    [Authorize(Roles = AppConstants.ADMIN)]
    [HttpPut("ChangeIsWaiting")]
    public async Task<IActionResult> ChangeIsWaiting([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.ChangeToWaiting(id, userId);
        return Ok(new Response<string>(!result, AppConstants.SUCCESS));
    }
    #endregion -- Controller --
}