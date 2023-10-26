using System.Net;
using System.Net.Mail;
using BusBookTicket.Application.MailKet.DTO.Request;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Newtonsoft.Json.Linq;

namespace BusBookTicket.Application.MailKet.Service;

public class MailService : IMailService
{
    #region -- Propertiess --

    #endregion -- Propertiess --

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var client = new MailjetClient("9c8946fcf499857d45c77bc73f69c324", "9334d8220626a45378a43afc9ddbdab0");

         MailjetRequest request = new MailjetRequest
         {
            Resource = Send.Resource
         };

         // construct your email with builder
         var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("dominhdung21082002@gmail.com"))
                .WithSubject(mailRequest.subject)
                .WithHtmlPart($"<h4>Hi {mailRequest.toMail}! Im Minh Dũng</h4>" +
                              "<p>You want change password?, Code id = </p>")
                .WithTo(new SendContact("20110620@student.hcmute.edu.vn"))
                .Build();

         // invoke API to send email
         var response = await client.SendTransactionalEmailAsync(email);

    }
}