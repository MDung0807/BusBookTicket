using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class BillItem
    {
        #region -- Properties --
        public int billItemID { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Bill? bill { get; set; }
        public TicketItem TicketItem { get; set; }
        #endregion -- Relationship --


    }
}
