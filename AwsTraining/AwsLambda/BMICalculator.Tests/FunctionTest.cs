using Xunit;
using Amazon.Lambda.TestUtilities;
using BMICalculator.Models;

namespace BMICalculator.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new BMICalculator.Function();
            var context = new TestLambdaContext();
            var data = new InputData
            {
                Height = 180,
                Weight = 90,
                Age = 34
            };
            var result = function.FunctionHandler(data, context);

            Assert.Equal(WeightInformation.Obesity, result);
        }
    }
}
