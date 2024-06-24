namespace BusBookTicket.Ticket.Services.BackgroundService;

public interface ITicketBackgroundService
{
    /// <summary>
    /// When Bus departure
    /// </summary>
    /// <returns></returns>
    Task ChangeIsWaitingTicket();
    
    /// <summary>
    /// When bus complete trip
    /// </summary>
    /// <returns></returns>
    Task ChangeIsCompleteTicket();

    /// <summary>
    /// Notification for customer before 6 hour
    /// </summary>
    /// <returns></returns>
    Task NotificationBefore6Hour();
    
    /// <summary>
    /// Notification for customer before 24 hour
    /// </summary>
    /// <returns></returns>
    Task NotificationBefore24Hour();
}