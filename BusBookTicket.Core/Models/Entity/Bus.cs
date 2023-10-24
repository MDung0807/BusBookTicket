﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Bus
    {
        public int busID { get; set; }
        public string? busNumber { get; set; }
        public int status { get; set; }
        #region -- Relationship --

        public Company? company { get; set; }
        public BusType? busType { get; set; }
        public HashSet<SeatItem> Seats { get; set; }
        public HashSet<BusStop> busStops { get; set; }
        public HashSet<Review> reviews { get; set; }

        #endregion -- Relationship --

    }
}