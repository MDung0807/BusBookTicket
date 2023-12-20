namespace BusBookTicket.Core.Models.Entity;

public class Ticket_RouteDetail : BaseEntity
{
    public Ticket Ticket { get; set; }
    public RouteDetail RouteDetail { get; set; }
}