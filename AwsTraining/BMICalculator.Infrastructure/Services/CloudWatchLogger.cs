using Amazon.Lambda.Core;

namespace BMICalculator.Infrastructure.Services
{
    public class CloudWatchLogger : ILogger
    {
        private readonly ILambdaContext _context;

        public CloudWatchLogger(ILambdaContext context)
        {
            _context = context;
        }

        public void LogError(string message)
        {
            _context.Logger.LogLine($"ERROR: {_context.AwsRequestId}::{_context.FunctionName} : {message}");
        }

        public void LogMessage(string message)
        {
            _context.Logger.LogLine($"INFORMATION: {_context.AwsRequestId}::{_context.FunctionName} : {message}");
        }
    }
}
