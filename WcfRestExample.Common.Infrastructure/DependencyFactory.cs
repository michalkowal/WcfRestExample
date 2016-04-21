using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;

namespace WcfRestExample.Common.Infrastructure
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class DependencyFactory
    {
        private static IUnityContainer _container;

        /// <summary>
        /// Public reference to the unity container which will 
        /// allow the ability to register instrances or take 
        /// other actions on the container.
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            private set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        static DependencyFactory()
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            var container = new UnityContainer();
            section.Configure(container);

            _container = container;
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            T ret = Container.Resolve<T>();

            return ret;
        }

        /// <summary>
        /// Resolves the type to an instance of the appropriate type.
        /// </summary>
        /// <param name="type">Type of object to return<</param>
        /// <returns></returns>
        public static object Resolve(Type type)
        {
            var ret = Container.Resolve(type);

            return ret;
        }
    }
}
