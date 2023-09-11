using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.BusStationManage.Models
{
    public class BusStation
    {
        public int busStationID { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? description { get; set; }
    }
}
