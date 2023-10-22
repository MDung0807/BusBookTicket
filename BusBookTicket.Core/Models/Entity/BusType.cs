using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class BusType
    {
        public int busTypeID {  get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int totalSeats { get; set; }
        public int status { get; set; }
        public HashSet<Bus>? buses { get; set; }
    }
}
