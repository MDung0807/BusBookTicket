namespace BusBookTicket.Core.Models.Entity;

public class Routes : BaseEntity
{
    #region --Properties --

    #endregion --Properties --

    #region -- Relationships --

    public BusStation BusStationStart;
    public BusStation BusStationEnd;

    #endregion -- Relationships --
}