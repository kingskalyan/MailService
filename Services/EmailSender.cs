using EmailServiceAPI.Models;
using EmailServiceAPI.Services.interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;

namespace EmailServiceAPI.Services
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

       
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        public EmailSender(IOptions<EmailSettings> options)
        {
            emailSettings = options.Value;
        }
        public async Task SendEmailAsync(Mailrequest mailrequest)
        {
            var mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(emailSettings.Email);
            mail.To.Add(MailboxAddress.Parse(mailrequest.To));
            mail.Subject = mailrequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailrequest.Body;
            mail.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(emailSettings.Host, emailSettings.Port, true); // Use SSL/TLS
                await smtp.AuthenticateAsync(new NetworkCredential(emailSettings.Email, emailSettings.Password));
                await smtp.SendAsync(mail);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
