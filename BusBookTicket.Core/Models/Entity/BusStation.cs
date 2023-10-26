namespace BusBookTicket.Core.Models.Entity
{
    public class BusStation
    {
        public int busStationID { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? description { get; set; }
        public int status { get; set; }

        public HashSet<Bill>? ticketStarts { get; set; }
        public HashSet<Bill>? ticketends { get; set; }
        public HashSet<BusStop>? busStops { get; set; }
    }
}
