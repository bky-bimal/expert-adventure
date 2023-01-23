using Amazon.DynamoDBv2.Model;
using OAT.DataExport.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OAT.DataExport.Handlers {
    public interface IClientsHandler {
        Task SendAsync(IEnumerable<Dictionary<string, AttributeValue>> items);
    }

    public class ClientsHandler : IClientsHandler {
        private static JsonSerializerOptions _serializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public async Task SendAsync(IEnumerable<Dictionary<string, AttributeValue>> items) {
            var httpClient = new HttpClient();
            var client = new ClientBatch() {
                ClientSourceIds = items
                    .Select(item => new ClientSourceId() {
                        Timestamp = DateTime.UnixEpoch.AddSeconds(int.Parse(item.GetValueOrDefault("__destination_ts")?.N ?? "0")),
                        SourceId = int.Parse(item.GetValueOrDefault("pkALClient")?.N ?? "0"),
                        FirmName = item.GetValueOrDefault("ClientName")?.S,
                        Name = item.GetValueOrDefault("ClientName")?.S
                    })
                    .ToArray()
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://redtail-one.redtailtechnology.com:3000/redtailone/api/v1/orion_organizations/upsert") // Remove the port for production
            {
                Content = JsonContent.Create(client, mediaType: new("application/json"), options: _serializerOptions),
                Headers =
                {
                    { "x-partner-token", "2vXYnF66Jofg7XEf5Du6EyzE" },
                    { "x-partner-auth-token", "D9SLfREdCw8bGZYXzYSngox3" },
                }
            };

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}