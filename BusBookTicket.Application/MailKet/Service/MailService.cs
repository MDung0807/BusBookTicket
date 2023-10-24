using System.Net;
using System.Net.Mail;
using BusBookTicket.Application.MailKet.DTO.Request;

namespace BusBookTicket.Application.MailKet.Service;

public class MailService : IMailService
{
    #region -- Propertiess --

    #endregion -- Propertiess --

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var fromAddress = new MailAddress("dominhdung21082002@gmail.com", "Do Minh Dung");
        var toAddress = new MailAddress(mailRequest.toMail, "DoMinhDung");
        const string fromPassword = "D942925765EF338FDB82D6492DB23EC20DFA";
        const string subject = "Test email subject";
        const string body = "This is a test email.";

        var smtp = new SmtpClient
        {
            Host = "smtp.elasticemail.com",
            Port = 2525,
            Credentials = new NetworkCredential("dominhdung21082002@gmail.com", fromPassword)
        };
        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        await smtp.SendMailAsync(message);
        Console.WriteLine("Email sent successfully.");
    }
}