using System.Runtime.InteropServices.JavaScript;
using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Paging;
using BusBookTicket.Ticket.Services.TicketServices;
using BusBookTicket.Ticket.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Ticket.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    #region -- Properties --

    private readonly ITicketService _ticketService;
    #endregion -- Properties --

    public TicketController(ITicketService service)
    {
        this._ticketService = service;
    }

    #region -- Controller --

    [HttpPost("find")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTicket([FromBody] SearchForm searchForm , [FromQuery] TicketPaging paging)
    {
        var validator = new SearchFormValidator();
        var result = await validator.ValidateAsync(searchForm);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        TicketPagingResult response = await _ticketService.GetAllTicket(searchForm, paging: paging);
        return Ok(new Response<TicketPagingResult>(false, response));
    }
    
    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        TicketResponse response = await _ticketService.GetById(id);
        return Ok(new Response<TicketResponse>(false, response));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Create([FromBody] TicketFormCreate request)
    {
        var validator = new TicketFormCreateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpDelete("delete")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Delete(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] TicketFormUpdate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Update(request, request.TicketId, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("ChangeCompleteStatus")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> ChangeCompleteStatus([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.ChangeCompleteStatus(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [HttpGet("getAll")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> GetAll([FromQuery] TicketPaging paging, [FromQuery] DateOnly month)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        TicketPagingResult result = await _ticketService.GetAll(month, userId, paging);
        return Ok(new Response<TicketPagingResult>(false, result));
    }
    
    [HttpGet("getAllOnDate")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> GetAllOnDate([FromQuery] TicketPaging paging, [FromQuery] DateOnly date)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        TicketPagingResult result = await _ticketService.GetAllTicketOnDate(userId, date, paging);
        return Ok(new Response<TicketPagingResult>(false, result));
    }
    #endregion -- Controller --
}