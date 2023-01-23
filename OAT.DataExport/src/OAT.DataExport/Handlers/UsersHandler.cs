using Amazon.DynamoDBv2.Model;
using OAT.DataExport.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OAT.DataExport.Handlers {
    public interface IUsersHandler {
        Task SendAsync(IEnumerable<Dictionary<string, AttributeValue>> items);
    }

    public class UsersHandler : IUsersHandler {
        private static JsonSerializerOptions _serializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public async Task SendAsync(IEnumerable<Dictionary<string, AttributeValue>> items) {
            var httpClient = new HttpClient();
            var client = new UserBatch() {
                UserSourceIds = items
                    .Select(item => new UserSourceId() {
                        SourceId = int.TryParse(item.GetValueOrDefault("pkUser")?.N, out var pkUser) ? pkUser : null,
                        OrgSourceId = int.TryParse(item.GetValueOrDefault("AlClientId")?.N, out var alClientId) ? alClientId : null,
                        Username = item.GetValueOrDefault("UserId")?.S,
                        Email = item.GetValueOrDefault("Email")?.S,
                        FirstName = item.GetValueOrDefault("FirstName")?.S,
                        LastName = item.GetValueOrDefault("LastName")?.S,
                        Phone = item.GetValueOrDefault("BusinessPhoneNumber")?.S ?? item.GetValueOrDefault("PhMobile")?.S,
                        Status = item.GetValueOrDefault("IsActive")?.BOOL == true ? ActiveStatus.Active : ActiveStatus.Disabled
                    })
                    .ToArray()
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://redtail-one.redtailtechnology.com:3000/redtailone/api/v1/orion_users/upsert") // Remove the port for production
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