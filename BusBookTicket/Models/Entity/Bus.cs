
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class Bus
    {
        public int busID { get; set; }
        public string? busNumber { get; set; }

        public int companyID { get; set; }
        public int busTypeID { get; set; }
        public Company? company { get; set; }
        public BusType? busType { get; set; }
        public HashSet<BusStop>? busStops { get; set; }

    }
}
