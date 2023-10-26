using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Bill
    {
        #region -- Properties --
        public int billID { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateDeparture { get; set; }
        public long totolPrice { get; set; }
        public int status { get; set; }
        #endregion -- Properties --

        #region -- Relationship -- 
        public BusStation? busStationStart { get; set; }
        public BusStation? busStationEnd { get; set; }
        public Customer? customer { get; set; }
        public HashSet<BillItem>? billItems { get; set; }
        public Discount? discount { get; set; }
        #endregion  -- Relationship -- 
    }
}
