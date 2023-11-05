namespace BusBookTicket.Core.Models.Entity
{
    public class BusStation : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }

        public HashSet<Bill>? TicketStarts { get; set; }
        public HashSet<Bill>? TicketEnds { get; set; }
        public HashSet<BusStop>? BusStops { get; set; }
    }
}
