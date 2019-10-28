using System;
using System.Net;
using Amazon.Lambda.Core;
using BMICalculator.Models;
using BMICalculator.Services;
using Newtonsoft.Json;

namespace BMICalculator
{
    public class CalculateBMIFunction
    {
        private readonly ICalculator _calculator;
        private ILogger _logger;
        private readonly IStore _store;

        public CalculateBMIFunction()
        {
            _calculator = new BodyMassIndexCalculator();
            _store = new DynamoDbStoreService();
        }

        private void SaveInformations(InputData input, CalculationResult result)
        {
            CalculationItem item = new CalculationItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Age = input.Age,
                Height = input.Height,
                Weight = input.Weight,
                BMI = Math.Round(result.BMI, 2),
                Description = result.Description.ToString()
            };

            _store.StoreAsync(item);
        }

        public Response FunctionHandler(InputData input, ILambdaContext context)
        {
            _logger = new CloudWatchLogger(context);

            CalculationResult result = null;

            if (input == null)
            {
                _logger.LogError("Input data cannot be null.");
            }

            _logger.LogMessage(JsonConvert.SerializeObject(input));

            try
            {
                result = _calculator.Calculate(input.Height, input.Weight, input.Age);
                SaveInformations(input, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            result = result ?? new CalculationResult
            {
                Description = CalculationDescription.NotCalculated.ToString()
            };

            _logger.LogMessage($"Calculation result: {result.BMI} - {result.Description}");

            var response = new Response
            {
                StatusCode = HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(result)
            };

            _logger.LogMessage($"Serialized response: {response.Body}");

            return response;
        }
    }
}
