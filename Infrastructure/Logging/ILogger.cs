﻿namespace apiFullUni.Infrastructure.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception exception);
    }
}
