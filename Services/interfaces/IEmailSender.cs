using EmailServiceAPI.Models;

namespace EmailServiceAPI.Services.interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
