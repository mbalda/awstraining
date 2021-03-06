using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using BMICalculator.Domain.Models;
using BMICalculator.Infrastructure.Services;
using Newtonsoft.Json;

namespace BMICalculator.Functions
{
    public class GetCalculationsFunction
    {
        private readonly IStore _store;
        private ILogger _logger;

        public GetCalculationsFunction()
        {
            _store = new DynamoDbStoreService();
        }

        public async Task<CalculationItem> FunctionHandler(string id, ILambdaContext context)
        {
            _logger = new CloudWatchLogger(context);

            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Item Id cannot be null or empty string.");

                return null;
            }

            var item = await _store.GetItemByIdAsync(id);

            if (item != null)
                _logger.LogMessage($"Object with Id: {item.Id} has been found.");
            else
                _logger.LogError($"Object with Id: {item.Id} has not been found.");

            return item;
        }
    }
}
