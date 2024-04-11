namespace BusBookTicket.Core.Models.Entity;

public class Event : BaseEntity
{
    #region --Properties --

    public string Type { get; set; }
    public string Text { get; set; }

    #endregion --Properties --

    #region -- Relationship--

    public HashSet<NotificationObject> NotificationObject { get; set; }

    #endregion -- Relationship--
}