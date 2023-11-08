using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Company : BaseEntity
    {
        #region -- Properties --
        public string? Name { get; set; }
        public string? Introduction { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }

        #endregion -- Properties --

        #region -- Relationship --
        public ICollection<Bus> Buses { get; set; } = new List<Bus>();
        public Account Account { get; set; }
        public HashSet<SeatType> SeatTypes { get; set; }
        public Ward Ward { get; set; }
        #endregion -- Relationship --
       
    }
}
