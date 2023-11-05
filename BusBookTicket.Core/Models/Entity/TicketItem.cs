namespace BusBookTicket.Core.Models.Entity
{
    public class TicketItem : BaseEntity
    {
        #region -- Properties --
        public string SeatNumber { get; set; }
        public int Price { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Ticket Ticket { get; set; }
        public BillItem BillItem { get; set; }
        #endregion -- Relationship --



    }
}
