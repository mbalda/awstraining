namespace BMICalculator.Infrastructure.Services
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogError(string message);
    }
}
