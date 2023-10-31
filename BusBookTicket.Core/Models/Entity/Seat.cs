namespace BusBookTicket.Core.Models.Entity;

public class Seat
{
    #region -- Properties --
    public int seatID { get; set; }
    public string seatNumber { get; set; }
    public int status { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    #endregion -- Properties --

    #region -- Relationship --
    public Bus bus { get; set; }
    public SeatType seatType { get; set; }
    #endregion -- Relationship --
}