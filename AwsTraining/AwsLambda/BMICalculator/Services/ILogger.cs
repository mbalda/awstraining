using Amazon.Lambda.Core;

namespace BMICalculator.Services
{
    public interface ILogger
    {
        void LogMessage(ILambdaContext context, string message);
    }
}
