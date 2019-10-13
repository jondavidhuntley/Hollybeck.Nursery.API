using Common.Logging.Log4Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;

namespace Common.Logging.Extensions
{
    public class Log4NetProvider : ILoggerProvider
    {
        #region private Members

        /// <summary>
        /// Logger Config Filename
        /// </summary>
        private readonly string Log4NetConfigFile;

        /// <summary>
        /// Log File Target Folder
        /// </summary>
        private readonly string Log4NetTargetLoggingPath;

        /// <summary>
        /// Net4Log Collection (Dictionary)
        /// </summary>
        private readonly ConcurrentDictionary<string, Log4NetLogger> Loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log4NetConfigFile">Log4Net Config Filename</param>
        public Log4NetProvider(string log4NetConfigFile)
        {
            Log4NetConfigFile = log4NetConfigFile;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log4NetConfigFile">Log4Net Config Filename</param>
        /// <param name="log4NetTargetLoggingPath">Log4Net Updated Logging Folder</param>
        public Log4NetProvider(string log4NetConfigFile, string log4NetTargetLoggingPath)
        {
            Log4NetConfigFile = log4NetConfigFile;
            Log4NetTargetLoggingPath = log4NetTargetLoggingPath;
        }

        #endregion

        #region ILoggerProvider Members

        /// <summary>
        /// Create Logger
        /// </summary>
        /// <param name="componentName">Component Name</param>
        /// <returns>Log4NetLogger Instance</returns>
        public ILogger CreateLogger(string componentName)
        {
            // Read Config File
            XmlElement configXml = Parselog4NetConfigFile(Log4NetConfigFile);

            // Create Logger
            Log4NetLogger logger = new Log4NetLogger(componentName, Log4NetTargetLoggingPath, configXml);

            // Add Logger to Logger Dictionary
            Loggers.GetOrAdd(componentName, logger);

            return logger;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Read Logger Config File
        /// </summary>
        /// <param name="filename">Config Filename & Path</param>
        /// <returns>Config XML (log4net node)</returns>
        private static XmlElement Parselog4NetConfigFile(string filename)
        {
            XmlDocument log4netConfig = new XmlDocument();

            if (!File.Exists(filename))
            {
                throw new Exception(String.Format("Log4net configuration file : {0} NOT FOUND!", filename));
            }

            log4netConfig.Load(File.OpenRead(filename));

            return log4netConfig["log4net"];
        }

        #endregion

        #region Disposal

        /// <summary>
        /// Disposal - Clear Loggers from Dictionary
        /// </summary>
        public void Dispose()
        {
            Loggers.Clear();
        }

        #endregion
    }
}