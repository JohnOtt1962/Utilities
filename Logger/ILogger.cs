namespace Utilities.Logger
{
    public interface IUtilLogger
    {
        Task Log(string message, string className, LoggerBase.LogLevel logLevel, ConnectionDetails conDetails);
        Task Log(string message, Exception ex, string className, LoggerBase.LogLevel logLevel, ConnectionDetails conDetails);
    }
}