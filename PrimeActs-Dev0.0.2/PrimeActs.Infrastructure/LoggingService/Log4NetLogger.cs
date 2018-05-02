#region

using System;
using log4net;
using log4net.Config;
using PrimeActs.Infrastructure.Extensions;
using System.Diagnostics;

#endregion

namespace PrimeActs.Infrastructure.Logging
{
    public class Log4NetLogger : ILogger
    {
        private static bool log4netConfigured;
        private static readonly object locker = new object();

        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    var callingType = new StackFrame(2, false).GetMethod().DeclaringType;
                    _logger = LogManager.GetLogger(callingType);
                }
                return _logger;
            }
        }


        //Need to define a root appender
        public Log4NetLogger()
        {
            lock (locker)
            {
                if (!log4netConfigured)
                {
                    XmlConfigurator.Configure();
                    log4netConfigured = true;
                }
            }
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error(Exception x)
        {
            Logger.Error(x.LogMessage());
        }

        public void Error(string message, Exception x)
        {
            Logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Logger.Fatal(x.LogMessage());
        }
    }
}