﻿
namespace BusBookTicket.Core.Models.Entity
{
    public class Bus : BaseEntity
    {
        public string? BusNumber { get; set; }
        #region -- Relationship --

        public Company Company { get; set; }
        public BusType BusType { get; set; }
        public HashSet<BusStop> BusStops { get; set; }
        public HashSet<Review> Reviews { get; set; }
        public HashSet<Seat> Seats { get; set; }
        public HashSet<Ticket> Tickets { get; set; }
        #endregion -- Relationship --

    }
}
