using BusBookTicket.Ticket.Services.TicketItemServices;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Ticket.Controllers;

[ApiController]
[Route("api/ticketItem")]
public class TicketItemController : ControllerBase
{
    #region -- Properties --

    private readonly ITicketItemService _service;

    #endregion -- Properties --

    public TicketItemController(ITicketItemService service)
    {
        this._service = service;
    }

    #region -- Controllers --
    #endregion -- Controllers --
}