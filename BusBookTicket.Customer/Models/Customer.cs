using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
    }
}
