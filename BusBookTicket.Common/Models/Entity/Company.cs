using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Models.Entity
{
    public class Company
    {
        public int companyID {  get; set; }
        public string? name { get; set; }
        public string? introduction { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public ICollection<Bus> buses { get; set; } = new List<Bus>();
        public Account account { get; set; }
        public HashSet<Review> reviews { get; set; }
    }
}
