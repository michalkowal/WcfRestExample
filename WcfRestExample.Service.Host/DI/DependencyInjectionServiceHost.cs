using System;
using System.ServiceModel.Web;

namespace WcfRestExample.Service.Host.DI
{
    /// <summary>
    /// This service host is used to set up the service behavior that replaces the instance provider to use dependency injection.
    /// </summary>
    public class DependencyInjectionServiceHost : WebServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionServiceHost"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public DependencyInjectionServiceHost(Type serviceType, Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        { }

        /// <summary>
        /// Opens the channel dispatchers.
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the on-open operation has to complete before timing out.</param>
        protected override void OnOpen(TimeSpan timeout)
        {
            Description.Behaviors.Add(new DependencyInjectionServiceBehavior());

            base.OnOpen(timeout);
        }
    }
}
