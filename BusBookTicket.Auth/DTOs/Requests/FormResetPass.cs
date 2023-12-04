using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.Auth.DTOs.Requests;

[ValidateNever]
public class FormResetPass
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}