using System;
using System.Diagnostics;
using TRIINKOM.Infrastructure;
using TRIINKOM.Infrastructure.Extensions;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure
{
    public class Log4NetLogger : ILog
    {
        private readonly log4net.ILog _log4NetLogger;


        public Log4NetLogger(log4net.ILog log4NetLogger)
        {
            log4NetLogger.ThrowIfArgumentNull("log4NetLogger");

            _log4NetLogger = log4NetLogger;
        }

        public bool IsDebugEnabled
        {
            get { return _log4NetLogger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log4NetLogger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log4NetLogger.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log4NetLogger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log4NetLogger.IsFatalEnabled; }
        }

        public void Debug(object message)
        {
            if (!_log4NetLogger.IsDebugEnabled)
                return;

            _log4NetLogger.Debug(
                CreateDebugMessage(message));
        }

        public void Info(object message)
        {
            _log4NetLogger.Info(message);
        }

        public void Warn(object message)
        {
            _log4NetLogger.Warn(message);
        }

        public void Error(object message)
        {
            _log4NetLogger.Error(message);
        }

        public void Fatal(object message)
        {
            _log4NetLogger.Fatal(message);
        }

        public void Debug(object message, Exception t)
        {
            _log4NetLogger.Debug(message, t);
        }

        public void Info(object message, Exception t)
        {
            _log4NetLogger.Info(message, t);
        }

        public void Warn(object message, Exception t)
        {
            _log4NetLogger.Warn(message, t);
        }

        public void Error(object message, Exception t)
        {
            _log4NetLogger.Error(message, t);
        }

        public void Fatal(object message, Exception t)
        {
            _log4NetLogger.Fatal(message, t);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (!_log4NetLogger.IsDebugEnabled)
                return;

            _log4NetLogger.Debug(
                CreateDebugMessage(string.Format(format, args)));
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log4NetLogger.InfoFormat(format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log4NetLogger.WarnFormat(format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log4NetLogger.ErrorFormat(format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log4NetLogger.FatalFormat(format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_log4NetLogger.IsDebugEnabled)
                return;

            _log4NetLogger.Debug(
                CreateDebugMessage(string.Format(provider, format, args)));
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4NetLogger.InfoFormat(provider, format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4NetLogger.WarnFormat(provider, format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4NetLogger.ErrorFormat(provider, format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4NetLogger.FatalFormat(provider, format, args);
        }

        private string CreateDebugMessage(object message)
        {
            var stackTrace = new StackTrace();           // get call stack
            var callingFrame = stackTrace.GetFrames()[2];
            var method = callingFrame.GetMethod();
            return string.Format("Class: {0}; Method: {1}; Message: {2}",
                method.DeclaringType, method.Name, message);
        }

    }
}