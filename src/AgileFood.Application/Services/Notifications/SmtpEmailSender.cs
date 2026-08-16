using System.Net;
using System.Net.Mail;
using AgileFood.Application.Configuration;
using AgileFood.Application.Interfaces.Notifications;
using Microsoft.Extensions.Options;

namespace AgileFood.Application.Services.Notifications;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;

    public SmtpEmailSender(IOptions<SmtpSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendAsync(string toAddress, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(_settings.Host))
            return;

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.User, _settings.Password)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromName),
            Subject = subject,
            Body = body
        };

        message.To.Add(toAddress);

        await client.SendMailAsync(message);
    }
}
