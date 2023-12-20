namespace BusBookTicket.Core.Models.Entity;

public class PriceClassification : BaseEntity
{
    public Company Company { get; set; }
    public HashSet<RouteDetail> StopStationDetails { get; set; }
    public HashSet<Ticket> Tickets { get; set; }
}