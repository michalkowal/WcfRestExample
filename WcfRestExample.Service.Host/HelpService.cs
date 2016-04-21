using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Service.Host
{
    /// <summary>
    /// WCF Service with summary about all available services
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceContract]
    public class HelpService
    {
        private const string HTML_TEMPLATE =
            "<html xmlns=\"http://www.w3.org/1999/xhtml\">" + 
            "  <head>" +
            "    <title>Available services {0}</title>" +
            "    <style>BODY {{ color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }} #content {{ margin-left: 30px; font-size: .70em; padding-bottom: 2em; }} A:link {{ color: #336699; font-weight: bold; text-decoration: underline; }} A:visited {{ color: #6699cc; font-weight: bold; text-decoration: underline; }} A:active {{ color: #336699; font-weight: bold; text-decoration: underline; }} .heading1 {{ background-color: #003366; border-bottom: #336699 6px solid; color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal;margin: 0em 0em 10px -20px; padding-bottom: 8px; padding-left: 30px;padding-top: 16px;}} pre {{ font-size:small; background-color: #e5e5cc; padding: 5px; font-family: Courier New; margin-top: 0px; border: 1px #f0f0e0 solid; white-space: pre-wrap; white-space: -pre-wrap; word-wrap: break-word; }} table {{ border-collapse: collapse; border-spacing: 0px; font-family: Verdana;}} table th {{ border-right: 2px white solid; border-bottom: 2px white solid; font-weight: bold; background-color: #cecf9c;}} table td {{ border-right: 2px white solid; border-bottom: 2px white solid; background-color: #e5e5cc;}}</style>" +
            "  </head>" +
            "  <body>" +
            "    <div id=\"content\">" +
            "      <p class=\"heading1\">Available services {0}</p>" +
            "      <table>" +
            "        <tbody>" +
		    "        <tr>" +
            "          <th>Uri</th>" +
            "          <th>Name</th>" +
            "        </tr>" +
            "        {1}" +
            "        </tbody>" +
	        "      </table>" +
            "    </div>" +
            "  </body>" +
            "</html>";

        private const string ROW_TEMPLATE =
            "        <tr>" +
            "          <td>" +
            "            <a rel=\"operation\" href=\"{0}\">{1}</a>" +
            "          </td>" +
            "          <td>{2}</td>" +
            "        </tr>";

        /// <summary>
        /// Get summary
        /// </summary>
        /// <returns>HTML page with available services summary table</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/",
            BodyStyle = WebMessageBodyStyle.Bare)]
        public System.IO.Stream Help()
        {
            Host host = Host.Instance;
            Dictionary<Uri, string> availableServices = new Dictionary<Uri, string>();
            var context = WebOperationContext.Current;
            Uri baseUri = new Uri("http://localhost");

            if (context != null)
            {
                baseUri = context.IncomingRequest.UriTemplateMatch.BaseUri;

                foreach (IService service in host.Services)
                {
                    availableServices.Add(new Uri(baseUri, service.BaseRoute + "/"), service.GetType().Name);
                }
            }

            string rowsString = string.Empty;
            foreach (Uri url in availableServices.Keys)
            {
                string name = availableServices[url];

                rowsString += string.Format(ROW_TEMPLATE, new Uri(url, "help").AbsoluteUri, url.AbsoluteUri, name) + "\n";
            }

            string result = string.Format(HTML_TEMPLATE, baseUri.AbsoluteUri, rowsString);
            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return new MemoryStream(resultBytes);
        }
    }
}
