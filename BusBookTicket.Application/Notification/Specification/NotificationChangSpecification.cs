using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.Notification.Specification;

public class NotificationChangSpecification : BaseSpecification<NotificationChange>
{
    public NotificationChangSpecification(string actor) : base(x => x.Actor == actor, false){}
}