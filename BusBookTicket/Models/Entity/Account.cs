using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class Account
    {
        public int accountID { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }

        public virtual Customer customer { get; set; }
        public Company company { get; set; }
    }
}
