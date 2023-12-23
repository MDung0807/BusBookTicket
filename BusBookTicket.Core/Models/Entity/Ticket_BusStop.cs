namespace BusBookTicket.Core.Models.Entity;

public class Ticket_BusStop : BaseEntity
{
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int IndexStation { get; set; }
    public int DiscountPrice { get; set; }
    public Ticket Ticket { get; set; }
    
    // public HashSet<Bill> BillStarts { get; set; }
    // public HashSet<Bill> BillEnds { get; set; }
    public BusStop BusStop { get; set; }
}