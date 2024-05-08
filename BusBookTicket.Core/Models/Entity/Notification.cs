namespace BusBookTicket.Core.Models.Entity;

public class Notification : BaseEntity
{
    public string Notifier { get; set; }
    #region -- Relationship --
    
    public Company NotifierCompany { get; set; }
    public Customer NotifierCustomer { get; set; }
    public NotificationObject NotificationObject { get; set; }
    #endregion -- Relationship --
}