namespace BusBookTicket.Core.Models.Entity
{
    public class SeatItem
    {
        #region -- Properties --
        public int seatID { get; set; }
        public int seatNumber { get; set; }
        public int status { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Bus bus { get; set; }
        public BillItem? ticketItem { get; set; }
        #endregion -- Relationship --



    }
}
