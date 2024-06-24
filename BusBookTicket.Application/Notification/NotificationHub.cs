using BusBookTicket.Application.Notification.Paging;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Application.Notification.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification;

public class NotificationHub : Hub
{
    private static readonly Dictionary<string, int> ClientsNotification = new();

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<NotificationObject> _repository;
    private readonly INotificationService _notificationService;

    public NotificationHub(IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
        _repository = unitOfWork.GenericRepository<NotificationObject>();
    }
    public override async Task OnConnectedAsync()
    {
        try
        {
            string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
            var claims = JWTUtils.GetPrincipal(token);
            string role = claims.Claims.ElementAt(2).Value;
            string userId = claims.Claims.ElementAt(0).Value;

            await AddGroup(Context.ConnectionId, role, userId);
            await SendCountNotification(role: role, userId: userId);
            await base.OnConnectedAsync();
        }
        catch (Exception e)
        {
            await SendCountNotification("", "");
            throw;
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        try
        {
            string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
            var claims = JWTUtils.GetPrincipal(token);
            string role = claims.Claims.ElementAt(2).Value;
            string userId = claims.Claims.ElementAt(0).Value;
            await RemoveGroup(Context.ConnectionId, role, userId);
            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task GetNotifications()
    {
        try
        {
            string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
            var claims = JWTUtils.GetPrincipal(token);
            string role = claims.Claims.ElementAt(2).Value;
            string userId = claims.Claims.ElementAt(0).Value;
            var notifications = await _notificationService.GetNotification(actor: $"{role}_{userId}");
            await Clients.Group($"{role}_{userId}").SendAsync("ReceiveNotifications", notifications.Items);
        }
        catch (Exception e)
        {
            await Clients.Group("").SendAsync("ReceiveNotifications", 0);
        }

    }

    public async Task ReadNotification(int id)
    {
        try
        {
            string token = Context.GetHttpContext()?.Request.Query["access_token"].ToString() ?? throw new Exception();
            var claims = JWTUtils.GetPrincipal(token);
            string role = claims.Claims.ElementAt(2).Value;
            string userId = claims.Claims.ElementAt(0).Value;

            await _notificationService.SeenNotification(id, int.Parse(userId));

            await SendCountNotification(role, userId);
        }
        catch (Exception e)
        {
            await SendCountNotification("", "");
        }
    }

    private async Task SendCountNotification(string role, string userId)
    {
        //Add client into group with role
        NotificationObjectSpecification specification = new NotificationObjectSpecification(actor:$"{role}_{userId}",query: "COUNT_NOTIFICATION_NOT_SEEN");
        int countNotificationNotSeen = await _repository.Count(specification);
        await Clients.Group($"{role}_{userId}").SendAsync("ReceiveCountUnReadingNotification",countNotificationNotSeen );
    }

    private async Task AddGroup(string connectionId, string role, string userId)
    {
        switch (role)
        {
            case AppConstants.ADMIN:
                await Task.WhenAll( _notificationService.AddGroup(connectionId, AppConstants.ADMIN), 
                    _notificationService.AddGroup(connectionId, $"{AppConstants.ADMIN}_{userId}"));
                break;
            case AppConstants.COMPANY:
                await Task.WhenAll(_notificationService.AddGroup(connectionId, AppConstants.COMPANY),
                    _notificationService.AddGroup(connectionId, $"{AppConstants.COMPANY}_{userId}"));
                break;
            case AppConstants.CUSTOMER:
                await Task.WhenAll(_notificationService.AddGroup(connectionId, AppConstants.CUSTOMER), 
                    _notificationService.AddGroup(connectionId, $"{AppConstants.CUSTOMER}_{userId}"));
                break;
        }
    }
    
    private async Task RemoveGroup(string connectionId, string role, string userId)
    {
        switch (role)
        {
            case AppConstants.ADMIN:
                await _notificationService.RemoveGroup(connectionId, AppConstants.ADMIN);
                await _notificationService.RemoveGroup(connectionId, $"{AppConstants.ADMIN}_{userId}");
                break;
            case AppConstants.COMPANY:
                await _notificationService.RemoveGroup(connectionId, AppConstants.COMPANY);
                await _notificationService.RemoveGroup(connectionId, $"{AppConstants.COMPANY}_{userId}");
                break;
            case AppConstants.CUSTOMER:
                await _notificationService.RemoveGroup(connectionId, AppConstants.CUSTOMER);
                await _notificationService.RemoveGroup(connectionId, $"{AppConstants.CUSTOMER}_{userId}");
                break;
        }
    }
}