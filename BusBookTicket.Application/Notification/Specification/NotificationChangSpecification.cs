using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Application.Notification.Specification;

public class NotificationChangSpecification : BaseSpecification<NotificationChange>
{
    public NotificationChangSpecification(string actor, string query = default)
        : base(x => x.Actor == actor, false)
    {
        if (query == "COUNT_NOTIFICATION_NOT_SEEN")
        {
            Criteria = x => x.Status == (int)EnumsApp.NotSeen;
        }
    }
}