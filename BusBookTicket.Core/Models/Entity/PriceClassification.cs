namespace BusBookTicket.Core.Models.Entity;

public class PriceClassification : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public Company Company { get; set; }
    public HashSet<RouteDetail> StopStationDetails { get; set; }
    public HashSet<Ticket> Tickets { get; set; }
}