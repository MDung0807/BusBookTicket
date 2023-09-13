using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Models
{
    public class Account
    {
        public int accountID { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
    }
}
