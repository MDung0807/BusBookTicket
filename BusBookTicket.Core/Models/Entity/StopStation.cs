namespace BusBookTicket.Core.Models.Entity;

public class StopStation : BaseEntity
{
    #region -- RelationShip --

    public Bus Bus { get; set; }
    public RouteDetail RouteDetail { get; set; }
    public Prices Prices { get; set; }
    public HashSet<Ticket> Tickets { get; set; }

    #endregion -- RelationShip --
}