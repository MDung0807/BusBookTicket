namespace BusBookTicket.Core.Models.Entity;

public class NotificationObject : BaseEntity
{
    #region --Properties --

    public string Content { get; set; }
    public string Href { get; set; }

    public HashSet<Notification> Notifications { get; set; }
    public HashSet<NotificationChange> NotificationChanges { get; set; }
    #endregion --Properties --
    
    #region -- Relationship --
    public Event Event { get; set; }
    #endregion -- Relationship --
}