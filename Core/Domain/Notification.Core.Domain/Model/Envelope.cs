using System.Text.Json.Serialization;

namespace Notification.Core.Domain.Model;

public class Envelope
{
    [JsonPropertyName("message")]
    public Message? Message { get; set; }
}