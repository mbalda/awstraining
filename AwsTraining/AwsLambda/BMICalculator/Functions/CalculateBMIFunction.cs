using System;
using System.Net;
using Amazon.Lambda.Core;
using BMICalculator.Domain.Models;
using BMICalculator.Infrastructure.Services;
using Newtonsoft.Json;

namespace BMICalculator.Functions
{
    public class CalculateBmiFunction
    {
        private readonly ICalculator _calculator;
        private ILogger _logger;
        private readonly IStore _store;

        public CalculateBmiFunction()
        {
            _calculator = new BodyMassIndexCalculator();
            _store = new DynamoDbStoreService();
        }

        private string SaveInformations(InputData input, CalculationResult result)
        {
            CalculationItem item = new CalculationItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Age = input.Age,
                Height = input.Height,
                Weight = input.Weight,
                BMI = result.BMI,
                Description = result.Description.ToString()
            };

            _store.StoreAsync(item);

            return item.Id;
        }

        public CalculationResult FunctionHandler(InputData input, ILambdaContext context)
        {
            _logger = new CloudWatchLogger(context);

            CalculationResult result = null;

            if (input == null)
            {
                _logger.LogError("Input data cannot be null.");
            }
            else
            {
                _logger.LogMessage(JsonConvert.SerializeObject(input));

                try
                {
                    result = _calculator.Calculate(input.Height, input.Weight, input.Age);
                    result.Id = SaveInformations(input, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            result = result ?? new CalculationResult
            {
                Description = CalculationDescription.NotCalculated.ToString()
            };

            _logger.LogMessage($"Calculation result: {result.BMI} - {result.Description}");

            return result;
        }
    }
}
