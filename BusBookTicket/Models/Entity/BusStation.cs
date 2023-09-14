namespace BusBookTicket.Models.Entity
{
    public class BusStation
    {
        public int busStationID { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? description { get; set; }

        public HashSet<Ticket>? tickets { get; set; }
        public HashSet<BusStop>? busStops { get; set; }
    }
}
