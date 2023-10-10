using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Models.Entity
{
    public class Ticket
    {
        #region -- Properties --
        public int ticketID { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateDeparture { get; set; }
        public long totolPrice { get; set; }
        public int status { get; set; }
        #endregion -- Properties --

        #region -- Relationship -- 
        public BusStation? busStation { get; set; }
        public Customer? customer { get; set; }
        public HashSet<TicketItem>? ticketItems { get; set; }
        public Discount? discount { get; set; }
        #endregion  -- Relationship -- 
    }
}
