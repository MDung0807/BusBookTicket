using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Recevice Message", $"{Context.ConnectionId} Has Join ");
    }
}