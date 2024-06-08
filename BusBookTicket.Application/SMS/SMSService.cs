using System;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BusBookTicket.Application.SMS
{
    public class SMSService
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly IConfiguration _configuration;
        public SMSService()
        {
            // Lấy thông tin từ biến môi trường
            accountSid = ("ACe7f20f4ee2ff039eefaa915a01ef35ce");
            authToken = ("17984da0af2adcb57e2fe677861a7649");

            if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken))
            {
                throw new InvalidOperationException("Twilio credentials are not set in the environment variables.");
            }

            // Khởi tạo Twilio client
            TwilioClient.Init(accountSid, authToken);
        }

        public Task SendSms(string toPhoneNumber, string messageBody)
        {
            try
            {
                
                var messageResource = MessageResource.Create(
                    body: messageBody,
                    from: new Twilio.Types.PhoneNumber(""),
                    to: new Twilio.Types.PhoneNumber("")
                );

                return Task.FromResult(messageResource.Sid);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}