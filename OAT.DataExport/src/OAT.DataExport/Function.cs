using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using OAT.DataExport.Handlers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace OAT.DataExport;

public class Function {
    public async Task FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context) {
        if (dynamoEvent.Records is not null && dynamoEvent.Records.Count > 0) {
            context.Logger.LogInformation($"Beginning to process {dynamoEvent.Records.Count} records...");

            var newImages = dynamoEvent.Records.Select(x => x.Dynamodb.NewImage);

            if (dynamoEvent.Records.First().EventSourceArn.Contains("AlClient", StringComparison.OrdinalIgnoreCase)) {
                var clientHandler = new ClientsHandler();
                await clientHandler.SendAsync(newImages);
            } else {
                var userHandler = new UsersHandler();
                await userHandler.SendAsync(newImages);
            }

            context.Logger.LogInformation("Stream processing complete.");
        }
    }
}