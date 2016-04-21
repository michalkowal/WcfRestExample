using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Plugins.Logger
{
    /// <summary>
    /// WCF Logger Service contract
    /// </summary>
    [ServiceContract]
    public interface ILoggerService : IService
    {
        /// <summary>
        /// Add trace log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/trace",
            RequestFormat = WebMessageFormat.Json)]
        void Trace(LogDTO dto);

        /// <summary>
        /// Add debug log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/debug",
            RequestFormat = WebMessageFormat.Json)]
        void Debug(LogDTO dto);

        /// <summary>
        /// Add info log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/info",
            RequestFormat = WebMessageFormat.Json)]
        void Info(LogDTO dto);

        /// <summary>
        /// Add warn log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/warn",
            RequestFormat = WebMessageFormat.Json)]
        void Warn(LogDTO dto);

        /// <summary>
        /// Add error log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/error",
            RequestFormat = WebMessageFormat.Json)]
        void Error(LogDTO dto);

        /// <summary>
        /// Add fatal log
        /// </summary>
        /// <param name="dto">Client info to logging</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/fatal",
            RequestFormat = WebMessageFormat.Json)]
        void Fatal(LogDTO dto);
    }

    /// <summary>
    /// DTO with basic information to log
    /// </summary>
    [DataContract]
    public class LogDTO
    {
        /// <summary>
        /// Client module name
        /// </summary>
        [DataMember]
        public string Module
        {
            get; set;
        }

        /// <summary>
        /// Client function who want to log
        /// </summary>
        [DataMember]
        public string Function
        {
            get; set;
        }

        /// <summary>
        /// Client message to log
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message
        {
            get; set;
        }
    }
}
