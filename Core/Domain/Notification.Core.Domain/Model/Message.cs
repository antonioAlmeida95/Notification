using System.Text.Json.Serialization;

namespace Notification.Core.Domain.Model;

public class Message
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("nome")]
    public string? Nome { get; set; }
    [JsonPropertyName("origem")]
    public string? Origem { get; set; }
    [JsonPropertyName("notificationId")]
    public Guid? NotificationId { get; set; }
}