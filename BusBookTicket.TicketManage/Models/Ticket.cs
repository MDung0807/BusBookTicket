using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.TicketManage.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ticketID { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateDeparture { get; set; }
        public long totolPrice { get; set; }

    }
}
