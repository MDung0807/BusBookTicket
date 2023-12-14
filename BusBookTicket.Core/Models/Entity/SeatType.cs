namespace BusBookTicket.Core.Models.Entity;

public class SeatType : BaseEntity
{
    #region -- Poperties --
    public string Type { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool IsCommon { get; set; }
    #endregion -- Properties --

    #region -- Relationships --
    public HashSet<Seat> Seats { get; set; }
    public Company Company { get; set; }
    #endregion -- Relationships --
}