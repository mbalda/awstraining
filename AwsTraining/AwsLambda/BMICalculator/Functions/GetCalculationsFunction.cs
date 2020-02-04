using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using BMICalculator.Domain.Models;
using BMICalculator.Infrastructure.Services;

namespace BMICalculator
{
    public class GetCalculationsFunction
    {
        private readonly IStore _store;
        private ILogger _logger;

        public GetCalculationsFunction()
        {
            _store = new DynamoDbStoreService();
        }

        public async Task<Response> FunctionHandler(string id, ILambdaContext context)
        {
            _logger = new CloudWatchLogger(context);

            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Item Id cannot be null or empty string.");

                return new Response
                {
                    StatusCode = HttpStatusCode.BadGateway,
                    Body = "'Message' : 'Input cannot be null.'"
                };
            }

            var item = await _store.GetItemByIdAsync(id);

            if (item != null)
                _logger.LogMessage($"Object with Id: {item.Id} has been found.");
            else
                _logger.LogError($"Object with Id: {item.Id} has not been found.");

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item)
            };
        }
    }
}
