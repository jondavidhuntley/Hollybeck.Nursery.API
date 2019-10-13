using Microsoft.Extensions.Logging;

namespace Common.Logging.Extensions
{
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Add new Log4Net Provider with Specific Config File
        /// </summary>
        /// <param name="factory">Logger Factory (MS Extensions)</param>
        /// <param name="log4NetConfigFile">Config Filename with Path</param>
        /// <returns></returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile));
            return factory;
        }

        /// <summary>
        /// Add new Log4Net Provider with Specific Config File + Updated Logging Folder
        /// </summary>
        /// <param name="factory">Logger Factory (MS Extensions)</param>
        /// <param name="log4NetConfigFile">Config Filename with Path</param>
        /// <param name="updatedLoggingDirectory">Updated Logging Directory</param>
        /// <returns></returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile, string updatedLoggingDirectory)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile, updatedLoggingDirectory));
            return factory;
        }

        /// <summary>
        /// Add new Log4Net Provider - Reads Local log4net.config
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetProvider("log4net.config"));
            return factory;
        }
    }
}