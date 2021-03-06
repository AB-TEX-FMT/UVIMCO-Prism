﻿using Microsoft.Extensions.Logging;

namespace DataRepository.Repositories
{
    public class BaseRepository : IErrorLogging
    {
        #region Class Setup
        protected readonly ILogger _logger;

        protected BaseRepository(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogCritical(string message)
        {
            _logger.LogCritical(message);
        }
    }
}
