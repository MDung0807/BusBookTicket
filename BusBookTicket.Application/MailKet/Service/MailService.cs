using BusBookTicket.Application.MailKet.DTO.Request;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

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
                .WithSubject(mailRequest.Subject)
                .WithHtmlPart($@"
    <html>
    <head>
        <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
        <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
        <link href=""https://fonts.googleapis.com/css2?family=Libre+Franklin:wght@200&display=swap"" rel=""stylesheet"">
        <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0"" />
        <meta charset=""UTF-8"">
        <title>Email Content Beautification</title>
        <style>
            .body_content{{
                font-family: 'Libre Franklin', sans-serif;
                font-size: 14px;
                line-height: 1.6em;
                color: #666666;
                background-color: #ffffff;
                margin: 0;
                padding: 0;
                display: flex;
                margin: auto;
                justify-content: center;
            }}
            /* Add your CSS styles here to beautify the email content */
            .Title {{
                color: #9a5858;
                font-family: 'Libre Franklin', sans-serif;
                font-size: 25px;
                font-weight: bold;
                line-height: 24px;
                margin-bottom: 12px;
            }}
            .icon {{
                font-family: 'Material Symbols Outlined', sans-serif;
                font-size: 24px;
                line-height: 24px;
                margin-right: 12px;
                vertical-align: middle;
            }}
            .link {{
                color: #9a5858;
                text-decoration: none;
            }}
        </style>
    </head>
    <body>
        <div class=""body_content"">
            <div>
                <h1 class=""Title"">Hệ thống đặt xe trực tuyến</h1>
                <h2>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</h1>
                <p>Xin chào {mailRequest.FullName}!</p>
                <p>Chúng tôi xin gửi lời cảm ơn sâu sắc đến bạn vì đã chọn sử dụng dịch vụ của chúng tôi. Chúng tôi rất trân trọng và mong rằng bạn hài lòng với trải nghiệm của mình.</p>
                <p>Nếu bạn có bất kỳ câu hỏi hoặc phản hồi nào, đừng ngần ngại liên hệ với chúng tôi. Chúng tôi luôn sẵn sàng hỗ trợ bạn.</p>
                <p>Xin lưu ý rằng thông tin liên hệ của chúng tôi có sẵn dưới đây:</p>
                <ul>
                    <li><span class=""material-symbols-outlined icon""> mail </span><b>dominhdung</b></li>
                    <li><span class=""material-symbols-outlined icon""> call </span>0376177512</li>
                    <li><span class=""material-symbols-outlined icon""> link </span><a class=""link"" href=""https://www.facebook.com/DoMinhDung08"">https://www.facebook.com/DoMinhDung08</a></li>       
                </ul>
    
                <div>
                    <h3>Dịch vụ mà bạn đang dùng là: {mailRequest.Subject}</h1>
                        <p>{mailRequest.Message}</p>
                    {mailRequest.Content}
                </div>
                <p>Chân thành cảm ơn,</p>
                <p>Đội ngũ hỗ trợ khách hàng</p>
                <i>Đây là mail tự động, vui lòng không phản hồi lại mail này</i>
                <p>Dũng</p>
                <p>© 2021 Hệ thống đặt xe trực tuyến</p>
            </div>
        </div>
    </body>
    </html>
")
                .WithTo(new SendContact(mailRequest.ToMail))
                .Build();

         // invoke API to send email
         var response = await client.SendTransactionalEmailAsync(email);

    }

    public async Task SendEmailsAsync(List<MailRequest> mailRequests)
    {
         var client = new MailjetClient("9c8946fcf499857d45c77bc73f69c324", "9334d8220626a45378a43afc9ddbdab0");

         MailjetRequest request = new MailjetRequest
         {
            Resource = Send.Resource
         };
         List<TransactionalEmail> emails = new List<TransactionalEmail>();
         foreach (var mailRequest in mailRequests)
         {
             // construct your email with builder
             TransactionalEmail email = new TransactionalEmailBuilder()
                 .WithFrom(new SendContact("dominhdung21082002@gmail.com"))
                 .WithSubject(mailRequest.Subject)
                 .WithHtmlPart($@"
            <html>
            <head>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
                <link href=""https://fonts.googleapis.com/css2?family=Libre+Franklin:wght@200&display=swap"" rel=""stylesheet"">
                <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0"" />
                <meta charset=""UTF-8"">
                <title>Email Content Beautification</title>
                <style>
                    .body_content{{
                        font-family: 'Libre Franklin', sans-serif;
                        font-size: 14px;
                        line-height: 1.6em;
                        color: #666666;
                        background-color: #ffffff;
                        margin: 0;
                        padding: 0;
                        display: flex;
                        margin: auto;
                        justify-content: center;
                    }}
                    /* Add your CSS styles here to beautify the email content */
                    .Title {{
                        color: #9a5858;
                        font-family: 'Libre Franklin', sans-serif;
                        font-size: 25px;
                        font-weight: bold;
                        line-height: 24px;
                        margin-bottom: 12px;
                    }}
                    .icon {{
                        font-family: 'Material Symbols Outlined', sans-serif;
                        font-size: 24px;
                        line-height: 24px;
                        margin-right: 12px;
                        vertical-align: middle;
                    }}
                    .link {{
                        color: #9a5858;
                        text-decoration: none;
                    }}
                </style>
            </head>
            <body>
                <div class=""body_content"">
                    <div>
                        <h1 class=""Title"">Hệ thống đặt xe trực tuyến</h1>
                        <h2>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</h1>
                        <p>Xin chào {mailRequest.FullName}!</p>
                        <p>Chúng tôi xin gửi lời cảm ơn sâu sắc đến bạn vì đã chọn sử dụng dịch vụ của chúng tôi. Chúng tôi rất trân trọng và mong rằng bạn hài lòng với trải nghiệm của mình.</p>
                        <p>Nếu bạn có bất kỳ câu hỏi hoặc phản hồi nào, đừng ngần ngại liên hệ với chúng tôi. Chúng tôi luôn sẵn sàng hỗ trợ bạn.</p>
                        <p>Xin lưu ý rằng thông tin liên hệ của chúng tôi có sẵn dưới đây:</p>
                        <ul>
                            <li><span class=""material-symbols-outlined icon""> mail </span><b>dominhdung</b></li>
                            <li><span class=""material-symbols-outlined icon""> call </span>0376177512</li>
                            <li><span class=""material-symbols-outlined icon""> link </span><a class=""link"" href=""https://www.facebook.com/DoMinhDung08"">https://www.facebook.com/DoMinhDung08</a></li>       
                        </ul>
            
                        <div>
                            <h3>Dịch vụ mà bạn đang dùng là: {mailRequest.Subject}</h1>
                                <p>{mailRequest.Message}</p>
                            {mailRequest.Content}
                        </div>
                        <p>Chân thành cảm ơn,</p>
                        <p>Đội ngũ hỗ trợ khách hàng</p>
                        <i>Đây là mail tự động, vui lòng không phản hồi lại mail này</i>
                        <p>Dũng</p>
                        <p>© 2021 Hệ thống đặt xe trực tuyến</p>
                    </div>
                </div>
            </body>
            </html>
        ")
                .WithTo(new SendContact(mailRequest.ToMail))
                .Build();

             emails.Add(email);
         }
         // invoke API to send email
         var response = await client.SendTransactionalEmailsAsync(emails);
    }
}