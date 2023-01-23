using System.Text.Json.Serialization;

namespace OAT.DataExport.Models
{
    public class ClientBatch
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = "organization";

        [JsonPropertyName("source")]
        public string Source { get; set; } = "OC";

        [JsonPropertyName("source_info")]
        public ClientSourceId[] ClientSourceIds { get; set; } = Array.Empty<ClientSourceId>();
    }

    public class ClientSourceId
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("source_id")]
        public int SourceId { get; set; }

        [JsonPropertyName("firm_name")]
        public string? FirmName { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}