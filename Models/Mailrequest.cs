using MimeKit;

namespace EmailServiceAPI.Models
{
    public class Mailrequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
