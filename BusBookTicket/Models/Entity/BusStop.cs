namespace BusBookTicket.Models.Entity
{
    public class BusStop
    {
        public int busStopID { get; set; }

        public int busStationID { get; set; }
        public int busID { get; set; }
        public BusStation? BusStation { get; set; }
        public Bus? bus { get; set; }
    }
}
