namespace BusBookTicket.Common.Models.Entity
{
    public class BusStop
    {
        public int busStopID { get; set; }
        public int status { get; set; }
        public BusStation? BusStation { get; set; }
        public Bus? bus { get; set; }
    }
}
