using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfRestExample.Common.Data;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Plugins.Employees
{
    /// <summary>
    /// WCF Employee Service contract
    /// </summary>
    [ServiceContract]
    public interface IEmployeeService : IService
    {
        /// <summary>
        /// Find employees by params
        /// </summary>
        /// <param name="name">Employee's name</param>
        /// <param name="address">Employee's address</param>
        /// <param name="email">Employee's email</param>
        /// <param name="phone">Employee's phone</param>
        /// <returns>Filtered employees collection</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "?name={name}&address={address}&email={email}&phone={phone}",
            ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Employee> FindEmployees(string name, string address, string email, string phone);

        /// <summary>
        /// Get employee by ID
        /// </summary>
        /// <param name="id">Employee's Identified</param>
        /// <returns>Common employee entity</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Employee GetEmployee(string id);

        /// <summary>
        /// Save employee object
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Json)]
        void AddEmployee(Employee entity);

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "/{id}",
            RequestFormat = WebMessageFormat.Json)]
        void UpdateEmployee(string id, Employee entity);

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Json)]
        void DeleteEmployee(Employee entity);

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Employee's Identifier</param>
        [OperationContract]
        [WebInvoke(Method = "DELETE", 
            UriTemplate = "/{id}")]
        void DeleteEmployeeById(string id);
    }
}
