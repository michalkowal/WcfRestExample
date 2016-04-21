using NLog;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestExample.Common.Infrastructure
{
    public interface ILoggerExt: ILogger
    {
        void TraceMethod(MethodBase method, params object[] values);
        void TraceMethodResult(MethodBase method);
        void TraceMethodResult(MethodBase method, object returnValue);
        void ErrorMethod(Exception ex, MethodBase method);
    }

    public class LoggerWrapper : ILoggerExt
    {
        private Logger _logger;

        public LoggerWrapper(string logger)
            : this(LogManager.GetLogger(logger))
        {
        }

        public LoggerWrapper(Logger logger)
        {
            _logger = logger;
        }

        #region ILogger

        public LogFactory Factory
        {
            get
            {
                return _logger.Factory;
            }
        }

        public bool IsDebugEnabled
        {
            get
            {
                return _logger.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return _logger.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return _logger.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return _logger.IsInfoEnabled;
            }
        }

        public bool IsTraceEnabled
        {
            get
            {
                return _logger.IsTraceEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return _logger.IsWarnEnabled;
            }
        }

        public string Name
        {
            get
            {
                return _logger.Name;
            }
        }

        public event EventHandler<EventArgs> LoggerReconfigured;

        public void Debug([Localizable(false)] string message)
        {
            _logger.Debug(message);
        }

        public void Debug(object value)
        {
            _logger.Debug(value);
        }

        public void Debug(LogMessageGenerator messageFunc)
        {
            _logger.Debug(messageFunc);
        }

        public void Debug(IFormatProvider formatProvider, object value)
        {
            _logger.Debug(formatProvider, value);
        }

        public void Debug([Localizable(false)] string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Debug(string message, char argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, object argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, uint argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, sbyte argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, decimal argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, double argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, float argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, long argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, int argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, string argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, byte argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, bool argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(string message, ulong argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug([Localizable(false)] string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Debug(formatProvider, message, args);
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, object arg1, object arg2)
        {
            _logger.Debug(message, arg1, arg2);
        }

        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Debug(exception, message, args);
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            _logger.Debug(message, arg1, arg2, arg3);
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Debug(exception, formatProvider, message, args);
        }

        public void Debug<T>(T value)
        {
            _logger.Debug<T>(value);
        }

        public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Debug<TArgument>(message, argument);
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Debug<T>(formatProvider, value);
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Debug<TArgument>(formatProvider, message, argument);
        }

        public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Debug<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Debug<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Debug<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Debug<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void DebugException([Localizable(false)] string message, Exception exception)
        {
            _logger.DebugException(message, exception);
        }

        public void Error(object value)
        {
            _logger.Error(value);
        }

        public void Error([Localizable(false)] string message)
        {
            _logger.Error(message);
        }

        public void Error(LogMessageGenerator messageFunc)
        {
            _logger.Error(messageFunc);
        }

        public void Error([Localizable(false)] string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(IFormatProvider formatProvider, object value)
        {
            _logger.Error(formatProvider, value);
        }

        public void Error(string message, bool argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, byte argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, int argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, float argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, decimal argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, sbyte argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, ulong argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, uint argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, object argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, double argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, long argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, string argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(string message, char argument)
        {
            _logger.Error(message, argument);
        }

        public void Error([Localizable(false)] string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Error(formatProvider, message, args);
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, object arg1, object arg2)
        {
            _logger.Error(message, arg1, arg2);
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
            _logger.Error(message, arg1, arg2, arg3);
        }

        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Error(exception, formatProvider, message, args);
        }

        public void Error<T>(T value)
        {
            _logger.Error<T>(value);
        }

        public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Error<TArgument>(message, argument);
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Error<T>(formatProvider, value);
        }

        public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Error<TArgument>(formatProvider, message, argument);
        }

        public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Error<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Error<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Error<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Error<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void ErrorException([Localizable(false)] string message, Exception exception)
        {
            _logger.ErrorException(message, exception);
        }

        public void Fatal(object value)
        {
            _logger.Fatal(value);
        }

        public void Fatal([Localizable(false)] string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(LogMessageGenerator messageFunc)
        {
            _logger.Fatal(messageFunc);
        }

        public void Fatal(string message, double argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, decimal argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, object argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, uint argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, ulong argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, sbyte argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, float argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, char argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, byte argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, object value)
        {
            _logger.Fatal(formatProvider, value);
        }

        public void Fatal(string message, bool argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal([Localizable(false)] string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        public void Fatal(string message, string argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, int argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(string message, long argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal([Localizable(false)] string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Fatal(formatProvider, message, args);
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, object arg1, object arg2)
        {
            _logger.Fatal(message, arg1, arg2);
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Fatal(exception, message, args);
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            _logger.Fatal(message, arg1, arg2, arg3);
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Fatal(exception, formatProvider, message, args);
        }

        public void Fatal<T>(T value)
        {
            _logger.Fatal<T>(value);
        }

        public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Fatal<TArgument>(message, argument);
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Fatal<T>(formatProvider, value);
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Fatal<TArgument>(formatProvider, message, argument);
        }

        public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Fatal<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Fatal<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Fatal<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Fatal<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void FatalException([Localizable(false)] string message, Exception exception)
        {
            _logger.FatalException(message, exception);
        }

        public void Info(object value)
        {
            _logger.Info(value);
        }

        public void Info([Localizable(false)] string message)
        {
            _logger.Info(message);
        }

        public void Info(LogMessageGenerator messageFunc)
        {
            _logger.Info(messageFunc);
        }

        public void Info(string message, string argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, int argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, long argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, float argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, double argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, decimal argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, object argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, sbyte argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, uint argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, ulong argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, object value)
        {
            _logger.Info(formatProvider, value);
        }

        public void Info([Localizable(false)] string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void Info(string message, bool argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, char argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(string message, byte argument)
        {
            _logger.Info(message, argument);
        }

        public void Info([Localizable(false)] string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, object arg1, object arg2)
        {
            _logger.Info(message, arg1, arg2);
        }

        public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Info(formatProvider, message, args);
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Info(exception, message, args);
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
            _logger.Info(message, arg1, arg2, arg3);
        }

        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Info(exception, formatProvider, message, args);
        }

        public void Info<T>(T value)
        {
            _logger.Info<T>(value);
        }

        public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Info<TArgument>(message, argument);
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Info<T>(formatProvider, value);
        }

        public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Info<TArgument>(formatProvider, message, argument);
        }

        public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Info<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Info<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Info<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Info<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void InfoException([Localizable(false)] string message, Exception exception)
        {
            _logger.InfoException(message, exception);
        }

        public bool IsEnabled(LogLevel level)
        {
            return _logger.IsEnabled(level);
        }

        public void Log(LogEventInfo logEvent)
        {
            _logger.Log(logEvent);
        }

        public void Log(LogLevel level, object value)
        {
            _logger.Log(level, value);
        }

        public void Log(LogLevel level, [Localizable(false)] string message)
        {
            _logger.Log(level, message);
        }

        public void Log(LogLevel level, LogMessageGenerator messageFunc)
        {
            _logger.Log(level, messageFunc);
        }

        public void Log(Type wrapperType, LogEventInfo logEvent)
        {
            _logger.Log(wrapperType, logEvent);
        }

        public void Log(LogLevel level, string message, bool argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, float argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, double argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, decimal argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, object argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, byte argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, sbyte argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, uint argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, ulong argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, string argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, int argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, long argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, string message, char argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, object value)
        {
            _logger.Log(level, formatProvider, value);
        }

        public void Log(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            _logger.Log(level, message, exception);
        }

        public void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
        {
            _logger.Log(level, message, args);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2)
        {
            _logger.Log(level, message, arg1, arg2);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Log(level, formatProvider, message, args);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Log(level, exception, message, args);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
        {
            _logger.Log(level, message, arg1, arg2, arg3);
        }

        public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Log(level, exception, formatProvider, message, args);
        }

        public void Log<T>(LogLevel level, T value)
        {
            _logger.Log<T>(level, value);
        }

        public void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Log<TArgument>(level, message, argument);
        }

        public void Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
        {
            _logger.Log<T>(level, formatProvider, value);
        }

        public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Log<TArgument>(level, formatProvider, message, argument);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Log<TArgument1, TArgument2>(level, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Log<TArgument1, TArgument2>(level, formatProvider, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Log<TArgument1, TArgument2, TArgument3>(level, message, argument1, argument2, argument3);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Log<TArgument1, TArgument2, TArgument3>(level, formatProvider, message, argument1, argument2, argument3);
        }

        public void LogException(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            _logger.LogException(level, message, exception);
        }

        public void Swallow(Action action)
        {
            _logger.Swallow(action);
        }

        public void Swallow(Task task)
        {
            _logger.Swallow(task);
        }

        public T Swallow<T>(Func<T> func)
        {
            return _logger.Swallow<T>(func);
        }

        public T Swallow<T>(Func<T> func, T fallback)
        {
            return _logger.Swallow<T>(func, fallback);
        }

        public Task SwallowAsync(Func<Task> asyncAction)
        {
            return _logger.SwallowAsync(asyncAction);
        }

        public Task SwallowAsync(Task task)
        {
            return _logger.SwallowAsync(task);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return _logger.SwallowAsync<TResult>(asyncFunc);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
        {
            return _logger.SwallowAsync<TResult>(asyncFunc, fallback);
        }

        public void Trace(object value)
        {
            _logger.Trace(value);
        }

        public void Trace([Localizable(false)] string message)
        {
            _logger.Trace(message);
        }

        public void Trace(LogMessageGenerator messageFunc)
        {
            _logger.Trace(messageFunc);
        }

        public void Trace([Localizable(false)] string message, Exception exception)
        {
            _logger.Trace(message, exception);
        }

        public void Trace(string message, long argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, float argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, double argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, decimal argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, bool argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, object argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, sbyte argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, uint argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, ulong argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, object value)
        {
            _logger.Trace(formatProvider, value);
        }

        public void Trace(string message, char argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, byte argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, string argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(string message, int argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace([Localizable(false)] string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Trace(formatProvider, message, args);
        }

        public void Trace(string message, object arg1, object arg2)
        {
            _logger.Trace(message, arg1, arg2);
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Trace(exception, message, args);
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            _logger.Trace(message, arg1, arg2, arg3);
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Trace(exception, formatProvider, message, args);
        }

        public void Trace<T>(T value)
        {
            _logger.Trace<T>(value);
        }

        public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Trace<TArgument>(message, argument);
        }

        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Trace<T>(formatProvider, value);
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Trace<TArgument>(formatProvider, message, argument);
        }

        public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Trace<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Trace<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Trace<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Trace<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void TraceException([Localizable(false)] string message, Exception exception)
        {
            _logger.TraceException(message, exception);
        }

        public void Warn(object value)
        {
            _logger.Warn(value);
        }

        public void Warn([Localizable(false)] string message)
        {
            _logger.Warn(message);
        }

        public void Warn(LogMessageGenerator messageFunc)
        {
            _logger.Warn(messageFunc);
        }

        public void Warn(string message, uint argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, string argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, ulong argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, object value)
        {
            _logger.Warn(formatProvider, value);
        }

        public void Warn(string message, int argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, long argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, bool argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, float argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, char argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, double argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn([Localizable(false)] string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Warn(string message, decimal argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, object argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, byte argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(string message, sbyte argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn([Localizable(false)] string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Warn(formatProvider, message, args);
        }

        public void Warn(string message, object arg1, object arg2)
        {
            _logger.Warn(message, arg1, arg2);
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Warn(exception, message, args);
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
            _logger.Warn(message, arg1, arg2, arg3);
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            _logger.Warn(exception, formatProvider, message, args);
        }

        public void Warn<T>(T value)
        {
            _logger.Warn<T>(value);
        }

        public void Warn<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            _logger.Warn<TArgument>(message, argument);
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Warn<T>(formatProvider, value);
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            _logger.Warn<TArgument>(formatProvider, message, argument);
        }

        public void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Warn<TArgument1, TArgument2>(message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Warn<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Warn<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Warn<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
        }

        public void WarnException([Localizable(false)] string message, Exception exception)
        {
            _logger.WarnException(message, exception);
        }

        #endregion

        public void TraceMethod(MethodBase method, params object[] values)
        {
            string paramsString = FormatArguments(method.GetParameters(), values);
            string returnTypeName = (method is MethodInfo ? CSharpName(((MethodInfo)method).ReturnType) : Type.Missing.ToString());
            string msg = String.Format("{0} {1}({2})", returnTypeName, method.Name, paramsString);
            _logger.Debug(msg);
        }

        public void TraceMethodResult(MethodBase method)
        {
            string paramsString = FormatArguments(method.GetParameters());
            string returnTypeName = (method is MethodInfo ? CSharpName(((MethodInfo)method).ReturnType) : Type.Missing.ToString());
            string msg = String.Format("{0} {1}({2})", returnTypeName, method.Name, paramsString);
            _logger.Debug(msg);
        }

        public void TraceMethodResult(MethodBase method, object returnValue)
        {
            string resultString = FormatResult(returnValue);
            string paramsString = FormatArguments(method.GetParameters());
            string returnTypeName = (method is MethodInfo ? CSharpName(((MethodInfo)method).ReturnType) : Type.Missing.ToString());
            string msg = String.Format("{0} {1}({2}) returning {3}", returnTypeName, method.Name, paramsString, resultString);
            _logger.Debug(msg);
        }

        public void ErrorMethod(Exception ex, MethodBase method)
        {
            string msg = string.Format("{0} error", method.Name);
            _logger.Error(ex, msg);
        }

        private string FormatArguments(ParameterInfo[] paramsInfo, params object[] values)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                ParameterInfo arg = paramsInfo[i];
                string argName = arg.Name;
                string argType = arg.ParameterType.Name;

                if (values != null && values.Length > i)
                {
                    sb.AppendFormat("{0} {1}={2}", argType, argName, values[i] ?? "null");
                }
                else
                {
                    sb.AppendFormat("{0} {1}", argType, argName);
                }
                if (i < values.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        private string FormatResult(object value)
        {
            if (value != null && value is System.Collections.IEnumerable)
            {
                StringBuilder sb = new StringBuilder();
                System.Collections.IEnumerable colletion = value as System.Collections.IEnumerable;
                sb.Append("[ ");
                foreach (object el in colletion)
                {
                    sb.Append((el ?? "null") + ", ");
                }
                sb.Append(" ]");

                string result = sb.ToString();
                return (result.EndsWith(", ") ? result.Substring(0, result.Length - 2) : result);
            }
            else
            {
                string typeName = (value != null ? CSharpName(value.GetType()) : "");
                return string.Format("{0} {1}", typeName, value ?? "null");
            }
        }

        /// <summary>
        /// Get full type name with full namespace names
        /// </summary>
        /// <param name="type">
        /// The type to get the C# name for (may be a generic type or a nullable type).
        /// </param>
        /// <returns>
        /// Full type name, fully qualified namespaces
        /// </returns>
        private string CSharpName(Type type)
        {
            Type nullableType = Nullable.GetUnderlyingType(type);
            string nullableText;
            if (nullableType != null)
            {
                type = nullableType;
                nullableText = "?";
            }
            else
            {
                nullableText = string.Empty;
            }

            if (type.IsGenericType)
            {
                return string.Format(
                    "{0}<{1}>{2}",
                    type.Name.Substring(0, type.Name.IndexOf('`')),
                    string.Join(", ", type.GetGenericArguments().Select(ga => CSharpName(ga))),
                    nullableText);
            }

            switch (type.Name)
            {
                case "String":
                    return "string";
                case "Int32":
                    return "int" + nullableText;
                case "Decimal":
                    return "decimal" + nullableText;
                case "Object":
                    return "object" + nullableText;
                case "Void":
                    return "void" + nullableText;
                default:
                    return (string.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName) + nullableText;
            }
        }
    }
}
