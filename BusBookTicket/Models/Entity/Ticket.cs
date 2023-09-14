using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class Ticket
    {
        public int ticketID { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateDeparture { get; set; }
        public long totolPrice { get; set; }
        
        public int busStationStartID { get; set; }
        public int busStationEndID { get; set; }
        public int customerID { get; set; }
        public int discountID { get; set; }

        public BusStation? busStation { get; set; }
        public Customer? customer { get; set; }
        public Review? review { get; set; }
        public HashSet<TicketItem>? ticketItem { get; set; }
        public Discount? discount { get; set; }
    }
}
