using BusBookTicket.Common.Common;
using BusBookTicket.TicketManage.DTOs.Requests;
using BusBookTicket.TicketManage.DTOs.Responses;
using BusBookTicket.TicketManage.Services.TicketItems;
using BusBookTicket.TicketManage.Services.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.TicketManage.Controllers;

public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ITicketItemService _ticketItemService;

    public TicketController(ITicketService ticketService, ITicketItemService ticketItemService)
    {
        this._ticketService = ticketService;
        this._ticketItemService = ticketItemService;
    }

    #region -- Controller --
    [Authorize(Roles = "CUSTOMER")]
    [HttpPost("create")]
    public async Task<IActionResult> createBill([FromBody] TicketRequest ticketRequest)
    {
        await _ticketService.create(ticketRequest);
        return Ok(new Response<string>(false, "Response"));
    }

    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getBill")]
    public async Task<IActionResult> getBill(int id)
    {
        TicketResponse ticketResponse = await _ticketService.getByID(id);
        return Ok(new Response<TicketResponse>(false, ticketResponse));
    }
    #endregion -- Controller --

}