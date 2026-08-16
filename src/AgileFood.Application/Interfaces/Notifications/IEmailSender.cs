namespace AgileFood.Application.Interfaces.Notifications;

public interface IEmailSender
{
    Task SendAsync(string toAddress, string subject, string body);
}
