namespace BusBookTicket.Core.Models.Entity
{
    public class Rank : BaseEntity
    {
        #region -- Properties --
        public string? Name { get; set; }
        public string? Description { get; set; }

        public HashSet<Customer>? Customers { get; set; }
        public HashSet<Discount>? Discounts { get; set; }
        #endregion -- Properties --
    }
}
