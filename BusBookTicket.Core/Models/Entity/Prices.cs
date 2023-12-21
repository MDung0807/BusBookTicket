namespace BusBookTicket.Core.Models.Entity;

public class Prices: BaseEntity
{
    public double Surcharges { get; set; }
    public double Price { get; set; }
    
    #region -- Relationship --

    public Routes Routes { get; set; }
    public Company Company { get; set; }

    #endregion -- Relationship --
}