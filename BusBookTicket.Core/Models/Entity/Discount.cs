using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Discount : BaseEntity
    {
        #region -- Properties --
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public float Value { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public HashSet<Bill>? Tickets { get; set; }
        public Rank? Rank { get; set; }
        #endregion -- Relationship -- M
    }
}
