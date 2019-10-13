using log4net;
using log4net.Appender;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Common.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        #region Private Members

        /// <summary>
        /// Component/Controller Name
        /// </summary>
        private readonly string ComponentName;

        /// <summary>
        /// Configuration Xml
        /// </summary>
        private readonly XmlElement ConfigXml;

        /// <summary>
        /// Log4net Logger
        /// </summary>
        private readonly ILog NetLog;

        /// <summary>
        /// Log4net Repository
        /// </summary>
        private ILoggerRepository LoggerRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="componentName">Component Name</param>
        /// <param name="updatedLoggingDirectory">Updated Logging Directory</param>
        /// <param name="configXml">Config XML</param>
        public Log4NetLogger(string componentName, string updatedLoggingDirectory, XmlElement configXml)
        {
            ComponentName = componentName;
            ConfigXml = configXml;

            // Create Log4Net Repo
            LoggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            // Create Logger
            NetLog = LogManager.GetLogger(LoggerRepository.Name, componentName);

            // Configure Logger
            log4net.Config.XmlConfigurator.Configure(LoggerRepository, configXml);

            // Update Target Logging Directory where specified
            if (!String.IsNullOrEmpty(updatedLoggingDirectory))
            {
                UpdateLoggingPath(updatedLoggingDirectory);
            }
        }

        #endregion

        #region ILogger Members

        /// <summary>
        /// Is Logger Enabled for Specific Log Level
        /// </summary>
        /// <param name="logLevel">Logging Level (Citical, Debug, Trace, Error, Info, Warning)</param>
        /// <returns>True or False</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return NetLog.IsFatalEnabled;

                case LogLevel.Debug:

                case LogLevel.Trace:
                    return NetLog.IsDebugEnabled;

                case LogLevel.Error:
                    return NetLog.IsErrorEnabled;

                case LogLevel.Information:
                    return NetLog.IsInfoEnabled;

                case LogLevel.Warning:
                    return NetLog.IsWarnEnabled;

                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        /// <summary>
        /// Logs an Event as specific Level (Citical, Debug, Trace, Error, Info, Warning)
        /// </summary>
        /// <typeparam name="TState">Entry to be Written</typeparam>
        /// <param name="logLevel">Entry will be written on this level</param>
        /// <param name="eventId">Id of the event</param>
        /// <param name="state">The entry to be written. Can be also an object</param>
        /// <param name="exception">The exception related to this entry</param>
        /// <param name="formatter">Function to create a string message of the state and exception</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = string.Empty;

            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            else
            {
                message = formatter(state, exception);
            }

            //
            // Log Event based on Log Level
            // Calls Net4Log Logger to Record Events
            //
            if (!String.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        NetLog.Fatal(message, exception);
                        break;

                    case LogLevel.Debug:

                    case LogLevel.Trace:
                        NetLog.Debug(message);
                        break;

                    case LogLevel.Error:
                        NetLog.Error(message, exception);
                        break;

                    case LogLevel.Information:
                        NetLog.Info(message);
                        break;

                    case LogLevel.Warning:
                        NetLog.Warn(message);
                        break;

                    default:
                        NetLog.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                        NetLog.Info(message, exception);
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Update Logging Folder
        /// </summary>
        /// <param name="newLogPath">New Log Path</param>
        private void UpdateLoggingPath(string newLogPath)
        {
            if (!String.IsNullOrEmpty(newLogPath))
            {
                Hierarchy h = (Hierarchy)LoggerRepository;
                foreach (IAppender a in h.Root.Appenders)
                {
                    if (a is FileAppender)
                    {
                        FileAppender fa = (FileAppender)a;

                        // Logging Filename is specified in Config
                        string logFilename = Path.GetFileName(fa.File);

                        // Create Logging Directory if necessary                       
                        if (!Directory.Exists(newLogPath))
                        {
                            Directory.CreateDirectory(newLogPath);
                        }

                        // Update Target Logging Path & Filename
                        fa.File = Path.Combine(newLogPath, logFilename);
                        fa.ActivateOptions();
                        break;
                    }
                }
            }
        }

        #endregion

        #region Disposal

        /// <summary>
        /// Disposal
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        #endregion
    }
}
