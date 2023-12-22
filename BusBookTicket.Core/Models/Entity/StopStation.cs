namespace BusBookTicket.Core.Models.Entity;

public class StopStation : BaseEntity
{
    #region -- RelationShip --

    public Bus Bus { get; set; }
    public Routes Route { get; set; }
    public HashSet<Ticket> Tickets { get; set; }

    #endregion -- RelationShip --
}