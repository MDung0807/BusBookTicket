using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.Auth.DTOs.Requests
{
    [ValidateNever]
    public class AuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
