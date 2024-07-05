using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Project.Configs;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);
        await client.SendMailAsync(mailMessage);
    }
}
