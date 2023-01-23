using System.Text.Json.Serialization;

namespace OAT.DataExport.Models
{
    public class UserBatch
    {
        [JsonPropertyName("source")]
        public string Source { get; set; } = "OC";

        [JsonPropertyName("source_info")]
        public UserSourceId[] UserSourceIds { get; set; } = Array.Empty<UserSourceId>();
    }

    public class UserSourceId
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("source_id")]
        public int? SourceId { get; set; }

        [JsonPropertyName("org_source_id")]
        public int? OrgSourceId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("middle_name")]
        public string? MiddleName { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("phone_ext")]
        public string? PhoneExtension { get; set; }

        [JsonPropertyName("individual_crd")]
        public string? IndividualCrd { get; set; }

        [JsonPropertyName("firm_crd")]
        public string? FirmCrd { get; set; }

        [JsonPropertyName("status")]
        public ActiveStatus Status { get; set; }
    }

    public enum ActiveStatus
    {
        Active = 0,
        Disabled = 1
    }
}