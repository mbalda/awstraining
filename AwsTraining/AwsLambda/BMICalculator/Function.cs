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
        private readonly BodyMassIndexCalculator _calculator;
        private readonly ILogger _logger;

        public Function()
        {
            _calculator = new BodyMassIndexCalculator();
            _logger = new CloudWatchLogger();
        }

        /// <summary>
        /// A simple function that calculates BMI for adult person
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public WeightInformation FunctionHandler(InputData input, ILambdaContext context)
        {
            if (input == null)
            {
                _logger.LogMessage(context, "Input data cannot be null.");

                throw new ArgumentNullException(nameof(input), "Input data cannot be null.");
            }

            try
            {
                return _calculator.Calculate(input.Height, input.Weight, input.Age);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(context, ex.Message);
            }

            return WeightInformation.NotCalculated;
        }
    }
}
