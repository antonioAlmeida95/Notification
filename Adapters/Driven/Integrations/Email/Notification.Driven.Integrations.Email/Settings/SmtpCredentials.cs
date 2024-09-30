namespace Notification.Driven.Integrations.Email.Settings;

public class SmtpCredentials
{
    public string? HostName { get; set; }
    public int Port { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
}