namespace BusBookTicket.Core.Models.Entity;

public class RouteDetail: BaseEntity
{
    #region -- Properties --

    public int IndexStation { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public int AddDay { get; set; }
    public double DiscountPrice { get; set; }
    #endregion -- Properties --
    #region -- Relationship --

    public Routes Routes { get; set; }
    public HashSet<Ticket_RouteDetail> TicketRouteDetails { get; set; }
    public Company Company { get; set; }
    public BusStation Station { get; set; }

    #endregion -- Relationship --
}