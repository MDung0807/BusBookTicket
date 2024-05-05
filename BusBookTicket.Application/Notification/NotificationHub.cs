using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification;

public class NotificationHub : Hub
{
    private static Dictionary<string, int> _clientsNotification = new();
    
    public override async Task OnConnectedAsync()
    {
        string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
        var claims = JWTUtils.GetPrincipal(token);
        string role = claims.Claims.ElementAt(2).ToString();
        string username = claims.Claims.ElementAt(1).ToString();
        
        await Groups.AddToGroupAsync(Context.ConnectionId, role+username);
        await Groups.AddToGroupAsync(Context.ConnectionId, role);

        //Add client into group with role
        
        await Clients.Group(username).SendAsync("ReceiveCountUnReadingNotification", 0);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
        string username = JWTUtils.GetUserName(token);
        _clientsNotification.Remove(username);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);
        await base.OnDisconnectedAsync(exception);
    }
}