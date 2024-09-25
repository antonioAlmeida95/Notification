using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notification.Core.Domain.Adapters.Integrations.Email;
using Notification.Driven.Integrations.Email.Settings;

namespace Notification.Driven.Integrations.Email.Notification;

public class NotificationEmail : INotificationEmail
{
    private readonly ILogger<NotificationEmail> _logger;
    private readonly SmtpCredentials _smtpCredentials;
    
    public NotificationEmail(ILogger<NotificationEmail> logger,
        IOptions<SmtpCredentials> options)
    {
        _logger = logger;
        _smtpCredentials = options.Value;
    }
    
    public bool SendEmail(string email, string subject, string body)
    {
        try
        {
            var client = ConfigureSmtpClient();
            ArgumentException.ThrowIfNullOrWhiteSpace(_smtpCredentials.UserName);
            
            using var mail = new MailMessage();
            mail.From = new MailAddress(_smtpCredentials.UserName);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            
            client.Send(message: mail);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SmtpClient ConfigureSmtpClient()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(_smtpCredentials.HostName);

        var smtpClient = new SmtpClient(_smtpCredentials.HostName, _smtpCredentials.Port);
        smtpClient.EnableSsl = _smtpCredentials.EnableSsl;
        smtpClient.UseDefaultCredentials = _smtpCredentials.UseDefaultCredentials;
        
        ArgumentException.ThrowIfNullOrWhiteSpace(_smtpCredentials.UserName);
        ArgumentException.ThrowIfNullOrWhiteSpace(_smtpCredentials.Password);

        smtpClient.Credentials = new NetworkCredential(_smtpCredentials.UserName, _smtpCredentials.Password);

        return smtpClient;
    }
}