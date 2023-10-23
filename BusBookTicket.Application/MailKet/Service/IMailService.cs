using BusBookTicket.Application.MailKet.DTO.Request;

namespace BusBookTicket.Application.MailKet.Service;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}