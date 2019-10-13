using System;
using Amazon.Lambda.Core;
using BMICalculator.Models;
using BMICalculator.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BMICalculator
{
    public class Function
    {
        private readonly ICalculator _calculator;
        private readonly ILogger _logger;
        private readonly IStore _store;

        public Function()
        {
            _calculator = new BodyMassIndexCalculator();
            _logger = new CloudWatchLogger();
            _store = new DynamoDbStoreService();
        }

        /// <summary>
        /// A simple function that calculates BMI for adult person
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public CalculationResult FunctionHandler(InputData input, ILambdaContext context)
        {
            CalculationResult result = null;

            if (input == null)
            {
                _logger.LogMessage(context, "Input data cannot be null.");

                throw new ArgumentNullException(nameof(input), "Input data cannot be null.");
            }

            try
            {
                result = _calculator.Calculate(input.Height, input.Weight, input.Age);
                SaveInformations(input, result);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(context, ex.Message);
            }

            return result ?? new CalculationResult
            {
                Description = CalculationDescription.NotCalculated
            };
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
                BMI = result.BMI,
                Description = result.Description.ToString()
            };

            _store.StoreAsync(item);
        }
    }
}
