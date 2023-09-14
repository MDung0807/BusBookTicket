using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class TicketItem
    {
        public int ticketItemID { get; set; }
        public int ticketID { get; set; }

        public int seatID { get; set; }
        public Ticket? ticket { get; set; }
        public SeatItem seat { get; set; }
    }
}
