using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
    }
    
    public async Task InsertNotification()
    {
        await _hubContext.Clients.Group(AppConstants.ADMIN).SendAsync("ReceiveNotification", "Thông báo này nè, bạn đã đọc được chưa");
    }

    public Task UpdateNotification()
    {
        throw new NotImplementedException();
    }
}