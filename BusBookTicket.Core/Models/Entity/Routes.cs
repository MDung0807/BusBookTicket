namespace BusBookTicket.Core.Models.Entity;

public class Routes : BaseEntity
{
    #region --Properties --

    #endregion --Properties --

    #region -- Relationships --

    public BusStation BusStationStart { get; set; }
    public BusStation BusStationEnd { get; set; }
    public RouteDetail RouteDetail { get; set; }
    public HashSet<StopStation> StopStations { get; set; }
    public Prices Prices { get; set; }

    #endregion -- Relationships --
}