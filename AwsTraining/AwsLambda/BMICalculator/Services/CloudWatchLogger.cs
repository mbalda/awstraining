using Amazon.Lambda.Core;

namespace BMICalculator.Services
{
    public class CloudWatchLogger : ILogger
    {
        public void LogMessage(ILambdaContext ctx, string msg)
        {
            ctx.Logger.LogLine($"{ctx.AwsRequestId}::{ctx.FunctionName} : {msg}");
        }
    }
}
