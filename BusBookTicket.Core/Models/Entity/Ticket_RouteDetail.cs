namespace BusBookTicket.Core.Models.Entity;

public class Ticket_RouteDetail : BaseEntity
{
    
    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public Ticket Ticket { get; set; }
    public RouteDetail RouteDetail { get; set; }
}