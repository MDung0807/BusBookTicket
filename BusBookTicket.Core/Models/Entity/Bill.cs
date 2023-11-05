
namespace BusBookTicket.Core.Models.Entity
{
    public class Bill: BaseEntity
    {
        #region -- Properties --
        public DateTime DateDeparture { get; set; }
        public long TotalPrice { get; set; }
        #endregion -- Properties --

        #region -- Relationship -- 
        public BusStation? BusStationStart { get; set; }
        public BusStation? BusStationEnd { get; set; }
        public Customer? Customer { get; set; }
        public HashSet<BillItem>? BillItems { get; set; }
        public Discount? Discount { get; set; }
        #endregion  -- Relationship -- 
    }
}
