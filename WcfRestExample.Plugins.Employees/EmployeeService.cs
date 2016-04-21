using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using WcfRestExample.Common.Data;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Plugins.Employees
{
    /// <summary>
    /// WCF Employee Service plugin implementation
    /// </summary>
    [Export(typeof(IService))]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private ILoggerExt _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeService()
            : this(DependencyFactory.Resolve<IRepository<Employee>>(), new LoggerWrapper(LogManager.GetCurrentClassLogger()))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepo">Employees repository</param>
        /// <param name="logger">Logger for current class</param>
        public EmployeeService(IRepository<Employee> employeeRepo, ILoggerExt logger)
        {
            _employeeRepository = employeeRepo;
            _logger = logger;
        }

        /// <summary>
        /// Get service base route
        /// </summary>
        public string BaseRoute
        {
            get
            {
                return "employee";
            }
        }

        /// <summary>
        /// Save employee object
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        public void AddEmployee(Employee entity)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), entity);

            try
            {
                if (IsValid(entity))
                {
                    _employeeRepository.Insert(entity);

                    SetResponseHttpStatus(HttpStatusCode.Created);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.Created);
                }
                else
                {
                    SetResponseHttpStatus(HttpStatusCode.BadRequest);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Employee's Identifier</param>
        public void DeleteEmployeeById(string id)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), id);

            try
            {
                bool deleted = _employeeRepository.Delete(Convert.ToInt32(id));

                if (deleted)
                {
                    SetResponseHttpStatus(HttpStatusCode.OK);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.OK);
                }
                else
                {
                    SetResponseHttpStatus(HttpStatusCode.NotFound);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        public void DeleteEmployee(Employee entity)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), entity);

            try
            {
                bool deleted = _employeeRepository.Delete(entity.EmployeeID);

                if (deleted)
                {
                    SetResponseHttpStatus(HttpStatusCode.OK);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.OK);
                }
                else
                {
                    SetResponseHttpStatus(HttpStatusCode.NotFound);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Find employees by params
        /// </summary>
        /// <param name="name">Employee's name</param>
        /// <param name="address">Employee's address</param>
        /// <param name="email">Employee's email</param>
        /// <param name="phone">Employee's phone</param>
        /// <returns>Filtered employees collection</returns>
        public IEnumerable<Employee> FindEmployees(string name, string address, string email, string phone)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), name, address, email, phone);

            try
            {
                IEnumerable<Employee> result = _employeeRepository.Find(e => 
                (String.IsNullOrEmpty(name) || e.Name.ToLower().Contains(name.ToLower())) &&
                (String.IsNullOrEmpty(address) || e.Address.ToLower().Contains(address.ToLower())) &&
                (String.IsNullOrEmpty(email) || e.Email.ToLower().Contains(email.ToLower())) &&
                (String.IsNullOrEmpty(phone) || e.PhoneNumber.ToLower().Contains(phone.ToLower())));

                SetResponseHttpStatus(HttpStatusCode.OK);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);

                return null;
            }
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        /// <param name="id">Employee's Identified</param>
        /// <returns>Common employee entity</returns>
        public Employee GetEmployee(string id)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), id);

            try
            {
                Employee result = _employeeRepository.GetById(Convert.ToInt32(id));

                if (result != null)
                {
                    SetResponseHttpStatus(HttpStatusCode.OK);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), result);
                }
                else
                {
                    SetResponseHttpStatus(HttpStatusCode.NotFound);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.NotFound);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);

                return null;
            }
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="entity">Common employee entity</param>
        public void UpdateEmployee(string id, Employee entity)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), entity);

            try
            {
                if (IsValid(entity) && id == entity.EmployeeID.ToString())
                {
                    bool updated = _employeeRepository.Update(entity);

                    if (updated)
                    {
                        SetResponseHttpStatus(HttpStatusCode.NoContent);
                        _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.NoContent);
                    }
                    else
                    {
                        SetResponseHttpStatus(HttpStatusCode.NotFound);
                        _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    SetResponseHttpStatus(HttpStatusCode.BadRequest);
                    _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorMethod(ex, MethodBase.GetCurrentMethod());

                SetResponseHttpStatus(HttpStatusCode.InternalServerError);
                _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), HttpStatusCode.InternalServerError);
            }
        }

        private bool IsValid(Employee entity)
        {
            if (entity == null)
                return false;

            if (string.IsNullOrEmpty(entity.Name))
                return false;

            if (string.IsNullOrEmpty(entity.Address))
                return false;

            return true;
        }

        /// <summary>
        /// Setting correct HTTP Return Code into outgoing response object
        /// </summary>
        /// <param name="statusCode">HTTP Status Code</param>
        private void SetResponseHttpStatus(HttpStatusCode statusCode)
        {
            var context = WebOperationContext.Current;
            if (context != null)
            {
                context.OutgoingResponse.StatusCode = statusCode;
            }
        }
    }
}
