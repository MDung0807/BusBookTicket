using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.DTOs.Requests
{
    public class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string roleName { get; set; }
    }
}
