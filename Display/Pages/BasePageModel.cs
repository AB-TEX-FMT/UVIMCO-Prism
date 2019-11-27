using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Display.Utilities.Interfaces;
using System;

namespace Display.Pages
{
    public class BasePageModel : PageModel, IErrorLogging
    {
        #region Class Setup
        protected readonly ILogger _logger;

        protected BasePageModel(ILogger logger)
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

        public bool IsAuthenticated { get; set; }

        // Holds any error messages for the View
        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get
            {
                if (String.IsNullOrEmpty(ErrorMessage))
                { return false; }
                return true;
            }
        }
    }
}
