using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.Auth.DTOs.Requests
{
    [ValidateNever]
    public class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string roleName { get; set; }
    }
}
