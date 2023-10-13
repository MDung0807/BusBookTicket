using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.DTOs.Responses
{
    public class ProfileResponse
    {
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public DateTime dateCreate { get; set; }
        public string roleName { get; set; }
        public string username { get; set; }
        public string rank { get; set; }
    }
}
