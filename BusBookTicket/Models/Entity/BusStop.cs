namespace BusBookTicket.Models.Entity
{
    public class BusStop
    {
        public int busStopID { get; set; }

        public BusStation? BusStation { get; set; }
        public Bus? bus { get; set; }
    }
}
