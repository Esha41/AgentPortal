using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.Helpers
{
    /// <summary>
    /// Extensions for <see cref="Microsoft.Extensions.Logging.ILogger"/>
    /// </summary>
    public static class LoggerHelper
    { 
        public static void LogError(this ILogger _logger, string eventMessage, Exception e, string requestId, string className)
        {
            var config = new Dictionary<string, object>();
            config.Add("requestId", requestId);
            config.Add("className", className);

            using (_logger.BeginScope(config))
            {
                _logger.LogError(e, "{0}. ResuestId: {1}", eventMessage, requestId);
            }
        }

        public static void LogError<T>(this ILogger _logger, string eventMessage, Exception e, string requestId, string methodName)
        {
            var className = typeof(T).Name + " : " + methodName;
            var config = new Dictionary<string, object>();
            config.Add("requestId", requestId);
            config.Add("className", className);

            using (_logger.BeginScope(config))
            {
                _logger.LogError(e, "{0}. ResuestId: {1}", eventMessage, requestId);
            }
        }
    }
}
