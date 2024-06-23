using BusBookTicket.Application.Notification.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Application.Notification.Specification;

public sealed class NotificationObjectSpecification : BaseSpecification<NotificationObject>
{
    public NotificationObjectSpecification(int id = default, string actor = default, NotificationPaging paging = default, bool checkStatus = default, string query = default) : 
        base(x => (x.Id == id || id == default) 
                  && ((actor == default) || x.NotificationChanges.Any(notiChange => notiChange.Actor == actor)),
            checkStatus: checkStatus)
    {
        if (query == "COUNT_NOTIFICATION_NOT_SEEN")
        {
            AddCriteria(x => x.Status == (int)EnumsApp.NotSeen);
        }
        AddInclude(x => x.Notifications);
        AddInclude(x => x.NotificationChanges.Where(notiChange => notiChange.Actor == actor));
        
        ApplyOrderByDescending(x => x.DateCreate);
        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}