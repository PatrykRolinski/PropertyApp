using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Common;

namespace PropertyApp.Application.Services.EmailService;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(EmailDto emailDto, IOptions<EmailSettings> config)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(config.Value.EmailUserName));
        email.To.Add(MailboxAddress.Parse(emailDto.To));
        email.Subject = emailDto.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = emailDto.Body };
        using var smtp = new SmtpClient();
        smtp.Connect(config.Value.EmailHost, 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(config.Value.EmailUserName, config.Value.EmailPassword);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
        
        
    }
}
