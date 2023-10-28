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

    [HttpGet("find")]
    [AllowAnonymous]
    public async Task<IActionResult> getTicket([FromQuery] DateTime dateTime, string stationStart, string stationEnd)
    {
        List<TicketResponse> response = await _ticketService.getAllTicket(dateTime, stationStart, stationEnd);
        return Ok(new Response<List<TicketResponse>>(false, response));
    }
    
    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> getByID([FromQuery] int id)
    {
        TicketResponse response = await _ticketService.getByID(id);
        return Ok(new Response<TicketResponse>(false, response));
    }

    [HttpPost("create")]
    public async Task<IActionResult> create([FromBody] TicketFormCreate request)
    {
        await _ticketService.create(request);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> delete([FromQuery] int id)
    {
        await _ticketService.delete(id);
        return Ok(new Response<string>(false, "Response"));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> update([FromBody] TicketFormUpdate request)
    {
        await _ticketService.update(request, request.ticketID);
        return Ok(new Response<string>(false, "Response"));
    }
    #endregion -- Controller --
}