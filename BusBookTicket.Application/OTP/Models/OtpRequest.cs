namespace BusBookTicket.Application.OTP.Models;

public class OtpRequest
{
    public string Email { get; set; }
    public string Code { get; set; }

    public OtpRequest(string email)
    {
        Email = email;
    }
}