namespace BusBookTicket.Core.Models.Entity
{
    public class TicketItem
    {
        #region -- Properties --
        public int ticketItemID { get; set; }
        public int seatNumber { get; set; }
        public int status { get; set; }
        public int price { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Ticket ticket { get; set; }
        public BillItem BillItem { get; set; }
        #endregion -- Relationship --



    }
}
