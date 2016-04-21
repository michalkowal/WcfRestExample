using NLog;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web;
using WcfRestExample.Common.Infrastructure;

namespace WcfRestExample.Service.Host
{
    public class Global : System.Web.HttpApplication
    {
        private ILoggerExt _logger = new LoggerWrapper(LogManager.GetCurrentClassLogger());

        protected void Application_Start(object sender, EventArgs e)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod());

            try
            {
                // Set MEF catalog
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(HttpRuntime.BinDirectory));

                // Create MEF container
                CompositionContainer container = new CompositionContainer(catalog);

                Host host = Host.Instance;
                // Load types marked by Export attribute
                container.ComposeParts(host);
                host.LoadServices(container);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error during Application_Start");
            }
            finally
            {
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod());
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Set CORS headers
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST, PUT, DELETE, GET, OPTIONS");

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}