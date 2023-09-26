using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.DTOs.Responses
{
    public class AuthResponse
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string token { get; set; }
        public string role { get; set; }

        public AuthResponse(int userID, string userName, string token, string role)
        {
            this.userID = userID;
            this.userName = userName;
            this.token = token;
            this.role = role;
        }

        public AuthResponse() { }
    }
}
