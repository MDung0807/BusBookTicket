using BusBookTicket.Application.Notification.Modal;

namespace BusBookTicket.Application.Notification.Services;

public interface INotificationService
{
    Task InsertNotification(AddNewNotification request, int userId);
    Task UpdateNotification(int id, int userId);
}