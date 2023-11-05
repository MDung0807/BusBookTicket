using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Ticket.Controllers;

[ApiController]
[Route("api/ticket")]
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
    public async Task<IActionResult> getTicket([FromBody] SearchForm searchForm )
    {
        List<TicketResponse> response = await _ticketService.GetAllTicket(searchForm);
        return Ok(new Response<List<TicketResponse>>(false, response));
    }
    
    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> getByID([FromQuery] int id)
    {
        TicketResponse response = await _ticketService.GetById(id);
        return Ok(new Response<TicketResponse>(false, response));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> create([FromBody] TicketFormCreate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Create(request, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> delete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Delete(id, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> update([FromBody] TicketFormUpdate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        await _ticketService.Update(request, request.ticketID, userId);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controller --
}