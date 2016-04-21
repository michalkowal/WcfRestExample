using NLog;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Plugins.Logger
{
    /// <summary>
    /// WCF Logger Service contract
    /// </summary>
    [Export(typeof(IService))]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoggerService : ILoggerService
    {
        private ILoggerExt _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerService()
            : this(new LoggerWrapper(LogManager.GetCurrentClassLogger()))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger for current class</param>
        public LoggerService(ILoggerExt logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get service base route
        /// </summary>
        public string BaseRoute
        {
            get
            {
                return "logger";
            }
        }

        /// <summary>
        /// Add debug log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Debug(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Debug(message);
        }

        /// <summary>
        /// Add error log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Error(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Error(message);
        }

        /// <summary>
        /// Add fatal log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Fatal(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Fatal(message);
        }

        /// <summary>
        /// Add info log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Info(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Info(message);
        }

        /// <summary>
        /// Add trace log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Trace(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Trace(message);
        }

        /// <summary>
        /// Add warn log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        public void Warn(LogDTO dto)
        {
            string message = PrepareMessage(dto);
            _logger.Warn(message);
        }

        /// <summary>
        /// Preparing correct message to log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        /// <returns>message with basic information to log</returns>
        private string PrepareMessage(LogDTO dto)
        {
            string message = string.Empty;

            if (!string.IsNullOrEmpty(dto.Message))
            {
                message += dto.Message;
            }
            if (!string.IsNullOrEmpty(dto.Function))
            {
                message = dto.Function + (dto.Function.EndsWith(")") ? "" : "()") + "|" + message;
            }
            if (!string.IsNullOrEmpty(dto.Module))
            {
                message = dto.Module + "|" + message;
            }

            message = GetClientEndpoint() + "|" + message;

            return message;
        }

        /// <summary>
        /// Getting sender IP address and port
        /// </summary>
        /// <returns>IP address with port</returns>
        private string GetClientEndpoint()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                MessageProperties messageProperties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpointProperty =
                  messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                return endpointProperty.Address.Replace("::1", "127.0.0.1") + ":" + endpointProperty.Port;
            }
            else
            {
                return "127.0.0.1";
            }
        }
    }
}
