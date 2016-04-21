using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using WcfRestExample.Common.Infrastructure;

namespace WcfRestExample.Service.Host.DI
{
    /// <summary>
    /// Produces <see cref="DependencyInjectionServiceHost"/>s.
    /// </summary>
    public class DependencyInjectionServiceHostFactory : WebServiceHostFactory
    {
        /// <summary>
        /// Creates a <see cref="DependencyInjectionServiceHost"/> for a specified type of service with a specific base address. 
        /// </summary>
        /// <returns>
        /// A <see cref="DependencyInjectionServiceHost"/> for the type of service specified with a specific base address.
        /// </returns>
        /// <param name="serviceType">
        /// Specifies the type of service to host. 
        /// </param>
        /// <param name="baseAddresses">
        /// The <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the service hosted.
        /// </param>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            //Register the service as a type so it can be found from the instance provider
            DependencyFactory.Container.RegisterType(serviceType);

            return new DependencyInjectionServiceHost(serviceType, baseAddresses);
        }
    }
}
