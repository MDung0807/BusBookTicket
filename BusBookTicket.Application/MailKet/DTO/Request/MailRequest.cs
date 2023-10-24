using Microsoft.AspNetCore.Http;

namespace BusBookTicket.Application.MailKet.DTO.Request;

public class MailRequest
{
    public string toMail { get; set; }
    public string subject { get; set; }
    public string body { get; set; }
}