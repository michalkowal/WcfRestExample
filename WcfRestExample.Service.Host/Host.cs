using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Web.Routing;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Service.Host
{
    /// <summary>
    /// WCF Plugins hosting mechanism
    /// </summary>
    public class Host
    {
        private ILoggerExt _logger = new LoggerWrapper(LogManager.GetCurrentClassLogger());

        private Host()
        {
        }

        private static Host _instance;
        public static Host Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Host();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Services collection injected by MEF
        /// </summary>
        [ImportMany]
        public IEnumerable<IService> Services { get; set; }

        /// <summary>
        /// Loading WCF services plugins by MEF container
        /// </summary>
        /// <param name="container"></param>
        public void LoadServices(CompositionContainer container)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod());

            try
            {
                foreach (var service in Services)
                {
                    try
                    {
                        RouteTable.Routes.Add(new ServiceRoute(service.BaseRoute,
                            new DI.DependencyInjectionServiceHostFactory(), service.GetType()));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, string.Format("Error during adding {0} to route {1}", service.GetType().Name, service.BaseRoute));
                    }
                }

                RouteTable.Routes.Add(new ServiceRoute("",
                    new DI.DependencyInjectionServiceHostFactory(), typeof(HelpService)));
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());
            }
            finally
            {
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod());
            }
        }
    }
}
