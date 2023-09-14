namespace BusBookTicket.Models.Entity
{
    public class SeatItem
    {
        public int seatID { get; set; }
        public int seatNumber { get; set; }

        public int busTypeID { get; set; }
        public BusType busType { get; set; }
        public TicketItem ticketItem { get; set; }
    }
}
