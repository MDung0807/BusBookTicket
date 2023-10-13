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
        public string username { get; set; }
        public string token { get; set; }
        public string roleName { get; set; }
        public string type { get; set; }

        public AuthResponse(int userID, string username, string token, string role)
        {
            this.userID = userID;
            this.username = username;
            this.token = token;
            this.roleName = role;
            this.type = "Bearer";
        }

        public AuthResponse() 
        {
            this.type = "Bearer";
        }
    }
}
