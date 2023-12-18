﻿namespace BusBookTicket.Core.Models.Entity
{
    public class BusStation : BaseEntity
    {
        #region -- Properties --

        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        #endregion -- Properties --

        public HashSet<BusStop> BusStops { get; set; }
        public Ward Ward { get; set; }
    }
}
