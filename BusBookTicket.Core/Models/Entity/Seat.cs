namespace BusBookTicket.Core.Models.Entity;

public class Seat
{
    #region -- Properties --

    public int seatID { get; set; }
    public int seatNumber { get; set; }
    public int status { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public string type { get; set;  }
    #endregion -- Properties --

    #region -- Relationship --
    public Bus bus { get; set; }
    #endregion -- Relationship --
}