using BusBookTicket.Ticket.Services.TicketServices;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Ticket.Controllers;

public class ReservePlace : Hub
{
    #region -- Properties --

    #endregion -- Properties --

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("");
    }
}