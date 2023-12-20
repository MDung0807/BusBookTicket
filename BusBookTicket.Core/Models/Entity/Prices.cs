namespace BusBookTicket.Core.Models.Entity;

public class Prices: BaseEntity
{
    #region -- Relationship --

    public Routes Routes { get; set; }
    public Company Company { get; set; }

    #endregion -- Relationship --
}