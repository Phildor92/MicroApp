using Microsoft.Extensions.Logging;

namespace MicroApp.Lib.Logging;

public static partial class LoggingService
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Debug, Message = "{message}")] 
    public static partial void Debug(this ILogger logger, string message);
    
    [LoggerMessage(EventId = 1, Level = LogLevel.Error, Message = "{message}")] 
    public static partial void Error(this ILogger logger, string message);
}