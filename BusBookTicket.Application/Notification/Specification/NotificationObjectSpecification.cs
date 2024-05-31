using BusBookTicket.Application.Notification.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.Notification.Specification;

public sealed class NotificationObjectSpecification : BaseSpecification<NotificationObject>
{
    public NotificationObjectSpecification(int id = default, string actor = default, NotificationPaging paging = default, bool checkStatus = default) : 
        base(x => (x.Id == id || id == default) 
                  && (actor == default) || x.NotificationChanges.Any(notiChange => notiChange.Actor == actor))
    {
        AddInclude(x => x.Notifications);
        AddInclude(x => x.NotificationChanges.Where(notiChange => notiChange.Actor == actor));
        
        ApplyOrderByDescending(x => x.DateCreate);
        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}