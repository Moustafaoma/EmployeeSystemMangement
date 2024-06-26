using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL.Services.SendEmail
{
	public class SendEmail : ISendEmail
	{
		private readonly IConfiguration _configuration;
        public SendEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string from, string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];
			var senderPassword = _configuration["EmailSettings:SenderPassword"];
			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(from);
			emailMessage.To.Add(recipients);
			emailMessage.Subject = subject;
			emailMessage.Body = $"<html><body>{body}</body></html>";
			emailMessage.IsBodyHtml = true;

			var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpHost"],int.Parse(_configuration["EmailSettings:SmtpPort"]))
			{
				Credentials = new NetworkCredential(senderEmail, senderPassword),
				EnableSsl = true
				
			};
			await smtpClient.SendMailAsync(emailMessage);
		}
	}
}
