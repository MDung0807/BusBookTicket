using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Discount
    {
        #region -- Properties --
        public int discountID { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int quantity { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int status { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public HashSet<Bill>? tickets { get; set; }
        public Rank? rank { get; set; }
        #endregion -- Relationship -- M
    }
}
