using BarberCode.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace BarberCode.Infra.Service.EmailServices;

public class EmailService : IEmailService
{
	private readonly EmailSettings _emailSettings;

	public EmailService(IOptions<EmailSettings> emailOptions)
	{
		_emailSettings = emailOptions.Value;
	}

	public async Task sendEmailAsync(string toEmail, string subject, string body)
	{
		var message = new MimeMessage();

		message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
		message.To.Add(new MailboxAddress("", toEmail));
		message.Subject = subject;

		var bodyBuilder = new BodyBuilder { HtmlBody = body };

		message.Body = bodyBuilder.ToMessageBody();
		using var client = new SmtpClient();
		await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
		await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
		await client.SendAsync(message);
		await client.DisconnectAsync(true);


	}

}
