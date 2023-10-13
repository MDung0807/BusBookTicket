using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Models.Entity
{
    public class TicketItem
    {
        #region -- Properties --
        public int ticketItemID { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Ticket? ticket { get; set; }
        public SeatItem seat { get; set; }
        #endregion -- Relationship --


    }
}
