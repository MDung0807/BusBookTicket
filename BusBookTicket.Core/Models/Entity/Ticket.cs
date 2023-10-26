namespace BusBookTicket.Core.Models.Entity;

public class Ticket
{
    #region -- Properties --
    public int ticketID { get; set; }
    public DateTime date { get; set; }
    #endregion -- Properties --

    #region -- Relationships --
    public Bus bus { get; set; }
    public HashSet<TicketItem> TicketItems { get; set; }
    #endregion -- Relationships --
}