namespace BusBookTicket.Core.Models.Entity;

public class Notification : BaseEntity
{
    #region 
    
    public Company NotifierCompany { get; set; }
    public Customer NotifierCustomer { get; set; }
    public NotificationObject NotificationObject { get; set; }
    #endregion
}