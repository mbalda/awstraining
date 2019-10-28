using Amazon.Lambda.Core;

namespace BMICalculator.Services
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogError(string message);
    }
}
