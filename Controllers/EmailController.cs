using EmailServiceAPI.Models;
using EmailServiceAPI.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] Mailrequest mailrequest )
        {
            try
            {
                await _emailSender.SendEmailAsync(mailrequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
