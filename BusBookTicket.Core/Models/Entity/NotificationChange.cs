namespace BusBookTicket.Core.Models.Entity;

public class NotificationChange : BaseEntity
{
    #region MyRegion

    public Customer ActorCustomer { get; set; }
    public Company ActorCompany { get; set; }
    public NotificationObject NotificationObject { get; set; }

    #endregion
}