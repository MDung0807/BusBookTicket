using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Utils;
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
    public async Task<IActionResult> GetAll()
    {
        int companyID = JwtUtils.GetUserID(HttpContext);
        List<SeatTypeResponse> responses = await _seatTypeService.getAll(companyID);
        return Ok(new Response<List<SeatTypeResponse>>(false, responses));
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
        int id = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = id;
        await _seatTypeService.Create(request, id);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _seatTypeService.Delete(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Update([FromBody] SeatTypeFormUpdate request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        request.CompanyId = id;
        await _seatTypeService.Update(request, request.Id, id);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPost("admin/create")]
    [Authorize(Roles = AppConstants.ADMIN)]
    public async Task<IActionResult> CreateByAdmin([FromBody] SeatTypeFormCreate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        int id = 0;
        request.CompanyId = id;
        await _seatTypeService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controllers --
}