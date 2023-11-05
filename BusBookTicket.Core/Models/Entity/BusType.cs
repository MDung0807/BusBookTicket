using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class BusType : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TotalSeats { get; set; }
        public HashSet<Bus>? Buses { get; set; }
    }
}
