using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.DTOs.Responses
{
    public class AccResponse
    {
        public int userID { get; set; }
        public string username { get; set; }
        public string roleName { get; set; }
        public int status { get; set; }

        public AccResponse(int userID, string username, string role)
        {
            this.userID = userID;
            this.username = username;
            this.roleName = role;
        }

        public AccResponse() { }
    }
}
