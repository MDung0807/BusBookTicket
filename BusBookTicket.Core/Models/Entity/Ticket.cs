namespace BusBookTicket.Core.Models.Entity;

public class Ticket : BaseEntity
{
    #region -- Properties --
    public DateTime Date { get; set; }
    #endregion -- Properties --

    #region -- Relationships --
    public Bus Bus { get; set; }
    public HashSet<TicketItem> TicketItems { get; set; }
    public HashSet<Ticket_BusStop> TicketBusStops { get; set; }
    #endregion -- Relationships --
}