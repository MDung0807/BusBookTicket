namespace BusBookTicket.Common.Models.Entity
{
    public class Rank
    {
        #region -- Properties --
        public int rankID {  get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public HashSet<Customer>? customers { get; set; }
        public HashSet<Discount>? discounts { get; set; }
        #endregion -- Properties --
    }
}
