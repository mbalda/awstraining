using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using BMICalculator.Infrastructure.Services;
using Newtonsoft.Json;

namespace BMICalculator.LambdaAndSaml.Functions
{
    public class GetCalculationsFunction
    {
        private readonly IStore _store;
        private ILogger _logger;

        public GetCalculationsFunction()
        {
            _store = new DynamoDbStoreService();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            _logger = new CloudWatchLogger(context);

            if (string.IsNullOrWhiteSpace(request.Body))
            {
                _logger.LogError("Item Id cannot be null or empty string.");

                return CreateResponse(HttpStatusCode.BadRequest, "Item Id cannot be null or empty string.");
            }

            var id = request.Body;

            var item = await _store.GetItemByIdAsync(id);

            if (item == null)
            {
                _logger.LogError($"Object with Id: {id} has not been found.");

                return CreateResponse(HttpStatusCode.NotFound, $"Object with Id: {id} has not been found.");
            }


            _logger.LogMessage($"Object with Id: {item.Id} has been found.");

            return CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(item));
        }

        private static APIGatewayProxyResponse CreateResponse(HttpStatusCode code, string body)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)code,
                Body = body,
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
    }
}
