using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.DTOs.Responses
{
    public class CustomerResponse
    {
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public DateTime dateCreate { get; set; }
    }
}
