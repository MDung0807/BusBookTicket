namespace BusBookTicket.Core.Models.Entity;

public class NotificationChange : BaseEntity
{
    public string Actor { get; set; }
    #region -- Relationship --

    public Customer ActorCustomer { get; set; }
    public Company ActorCompany { get; set; }
    public NotificationObject NotificationObject { get; set; }

    #endregion -- Relationship --
}