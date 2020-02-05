using Xunit;
using Amazon.Lambda.TestUtilities;
using BMICalculator.Domain.Models;
using BMICalculator.Functions;

namespace BMICalculator.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionIntegrationTest()
        {
            var function = new CalculateBmiFunction();
            var context = new TestLambdaContext();
            var data = new InputData
            {
                Height = 182,
                Weight = 90,
                Age = 34
            };
            var result = function.FunctionHandler(data, context);

            Assert.Equal("", result.Body);
        }
    }
}
