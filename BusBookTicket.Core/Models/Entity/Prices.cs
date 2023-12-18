namespace BusBookTicket.Core.Models.Entity;

public class Prices: BaseEntity
{
    #region -- Relationship --

    public Routes Routes;
    public PriceClassification PriceClassification;

    #endregion -- Relationship --
}