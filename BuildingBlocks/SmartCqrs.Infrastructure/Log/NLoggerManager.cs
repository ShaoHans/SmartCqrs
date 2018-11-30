using NLog;
using System;

namespace SmartCqrs.Infrastructure.Log
{
    public class NLoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(ex, message);
            }
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Warn(message);
            }
            else
            {
                logger.Warn(ex, message);
            }
        }
    }
}
