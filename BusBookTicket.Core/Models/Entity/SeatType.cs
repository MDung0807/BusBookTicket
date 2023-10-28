namespace BusBookTicket.Core.Models.Entity;

public class SeatType
{
    #region -- Poperties --
    public int typeID { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public int pirce { get; set; }
    public int status { get; set; }
    #endregion -- Properties --

    #region -- Relationships --
    public HashSet<Seat> seats { get; set; }
    public Company Company { get; set; }
    #endregion -- Relationships --
}