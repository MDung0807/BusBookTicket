namespace BusBookTicket.Application.Notification.Services;

public interface INotificationService
{
    Task InsertNotification();
    Task UpdateNotification();
}