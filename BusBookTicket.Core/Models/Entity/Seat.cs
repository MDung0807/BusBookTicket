namespace BusBookTicket.Core.Models.Entity;

public class Seat : BaseEntity
{
    #region -- Properties --
    public string SeatNumber { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    #endregion -- Properties --

    #region -- Relationship --
    public Bus Bus { get; set; }
    public SeatType SeatType { get; set; }
    #endregion -- Relationship --
}