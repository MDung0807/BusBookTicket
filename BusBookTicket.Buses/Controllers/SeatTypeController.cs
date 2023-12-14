using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.SeatType;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Buses.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/seatTypes")]
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
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> GetAll([FromQuery]SeatTypePaging paging)
    {
        int companyID = JwtUtils.GetUserID(HttpContext);
        SeatTypePagingResult responses = await _seatTypeService.GetAll(paging, companyID);
        return Ok(new Response<SeatTypePagingResult>(false, responses));
    }

    [HttpGet("getByID")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        SeatTypeResponse response = await _seatTypeService.GetById(id);
        return Ok(new Response<SeatTypeResponse>(false, response));
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Create([FromBody] SeatTypeFormCreate request)
    {
        var validator = new SeatTypeFormCreateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int id = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = id;
        request.IsCommon = false;
        await _seatTypeService.Create(request, id);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatTypeService.Delete(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("changeIsActive")]
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> ChangeIsActive([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatTypeService.ChangeIsActive(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("changeToDisable")]
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> ChangeIsDisable([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatTypeService.ChangeToDisable(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpPut("update")]
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> Update([FromBody] SeatTypeFormUpdate request)
    {
        var validator = new SeatTypeFormUpdateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int id = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = id;
        request.Status = 1;
        await _seatTypeService.Update(request, request.Id, id);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("admin/update")]
    [Authorize(Roles = $"{AppConstants.COMPANY}, {AppConstants.ADMIN}")]
    public async Task<IActionResult> UpdateByAdmin([FromBody] SeatTypeFormUpdate request)
    {
        var validator = new SeatTypeFormUpdateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int id = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = 0;
        request.Status = 1;
        await _seatTypeService.Update(request, request.Id, id);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPost("admin/create")]
    [Authorize(Roles = AppConstants.ADMIN)]
    public async Task<IActionResult> CreateByAdmin([FromBody] SeatTypeFormCreate request)
    {
        var validator = new SeatTypeFormCreateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        int id = 0;
        request.CompanyId = id;
        request.IsCommon = true;
        await _seatTypeService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controllers --
}