namespace BusBookTicket.Core.Models.Entity;

public class StopStation : BaseEntity
{
    #region -- RelationShip --

    public Bus Bus;
    public Routes Routes;
    public StopStationDetail StopStationDetail;

    #endregion -- RelationShip --
}