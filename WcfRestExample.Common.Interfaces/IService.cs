namespace WcfRestExample.Common.Interfaces
{
    /// <summary>
    /// Common WCF Service interface
    /// Currently used by MEF to load plugins
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Get service base route
        /// </summary>
        string BaseRoute
        {
            get;
        }
    }
}
