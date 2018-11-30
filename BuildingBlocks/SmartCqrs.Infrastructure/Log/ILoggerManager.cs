using System;

namespace SmartCqrs.Infrastructure.Log
{
    public interface ILoggerManager
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message, Exception ex = null);
        void Error(string message, Exception ex = null);
    }
}
