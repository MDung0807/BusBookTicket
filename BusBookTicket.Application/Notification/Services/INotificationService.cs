using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Paging;

namespace BusBookTicket.Application.Notification.Services;

public interface INotificationService
{
    Task InsertNotification(AddNewNotification request, int userId);
    Task UpdateNotification(int id, int userId);
    Task<NotificationResponse> SeenNotification(int id, int userId);
    Task<NotificationPagingResult> GetNotification(string actor);
    Task AddGroup(string connectionId, string group);
    Task RemoveGroup(string connectionId, string group);
    Task<List<int>> LoadGroup(int userId, bool checkStatus = true);
}