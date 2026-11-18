namespace Utilities.Logger
{
    public class Logger : LoggerBase, IUtilLogger
    {
        public async Task Log(string message, string className, LogLevel logLevel, ConnectionDetails conDetails, string? serviceId = null)
        {
            await WriteToLog(message, null, logLevel, className, conDetails, serviceId);
        }

        public async Task Log(string message, Exception ex, string className, LogLevel logLevel, ConnectionDetails conDetails, string? serviceId = null)
        {
            await WriteToLog(message, ex, logLevel, className, conDetails, serviceId);
        }
    }
}