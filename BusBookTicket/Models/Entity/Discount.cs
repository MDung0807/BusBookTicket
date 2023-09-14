using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class Discount
    {
        public int discountID { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int quantity { get; set; }
        public int requestRank { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public HashSet<Ticket>? tickets { get; set; }
    }
}
