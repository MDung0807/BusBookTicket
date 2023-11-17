using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.DTOs.Responses
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
        public string Type { get; set; }
        public string Avatar { get; set; }

        public AuthResponse(int userID, string username, string token, string role)
        {
            this.Id = userID;
            this.Username = username;
            this.Token = token;
            this.RoleName = role;
            this.Type = "Bearer";
        }

        public AuthResponse() 
        {
            this.Type = "Bearer";
        }
    }
}
