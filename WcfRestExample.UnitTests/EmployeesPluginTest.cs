using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using WcfRestExample.Common.Data;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Common.Interfaces;
using WcfRestExample.Plugins.Employees;

namespace WcfResExample.UnitTests
{
    [TestFixture]
    public class EmployeesPluginTest
    {
        private ILoggerExt _mockLogger;
        private IRepository<Employee> _mockRepo;
        private EmployeeService _sut;

        [SetUp]
        public void Init()
        {
            _mockLogger = Substitute.For<ILoggerExt>();
            _mockRepo = Substitute.For<IRepository<Employee>>();

            _sut = new EmployeeService(_mockRepo, _mockLogger);
        }

        [Test]
        public void BaseRouteTest()
        {
            Assert.AreEqual("employee", _sut.BaseRoute);
        }

        [Test]
        public void FindEmployeesMethodTest_GetAll()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.Find(Arg.Any<Func<Employee, bool>>()).Returns(stub);

            IEnumerable<Employee> result = _sut.FindEmployees(null, null, "", "");

            Assert.AreEqual(result.Count(), 3);
            Assert.AreSame(stub[0], result.ElementAt(0));
            Assert.AreSame(stub[1], result.ElementAt(1));
            Assert.AreSame(stub[2], result.ElementAt(2));

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void FindEmployeesMethodTest_GetAllByNameAndAddress()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.Find(Arg.Any<Func<Employee, bool>>()).Returns(x => stub.Where((Func<Employee, bool>)x[0]));

            IEnumerable<Employee> result = _sut.FindEmployees("empL", "Addr", null, null);

            Assert.AreEqual(result.Count(), 3);
            Assert.AreSame(stub[0], result.ElementAt(0));
            Assert.AreSame(stub[1], result.ElementAt(1));
            Assert.AreSame(stub[2], result.ElementAt(2));

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void FindEmployeesMethodTest_GetByEmail()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.Find(Arg.Any<Func<Employee, bool>>()).Returns(x => stub.Where((Func<Employee, bool>)x[0]));

            IEnumerable<Employee> result = _sut.FindEmployees("", "", "Email2", null);

            Assert.AreEqual(result.Count(), 1);
            Assert.AreSame(stub[1], result.ElementAt(0));

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void FindEmployeesMethodTest_GetByPhone()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.Find(Arg.Any<Func<Employee, bool>>()).Returns(x => stub.Where((Func<Employee, bool>)x[0]));

            IEnumerable<Employee> result = _sut.FindEmployees("", "", "", "555");

            Assert.AreEqual(result.Count(), 2);
            Assert.AreSame(stub[0], result.ElementAt(0));
            Assert.AreSame(stub[2], result.ElementAt(1));

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void FindEmployeesMethodTest_GetEmpty()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.Find(Arg.Any<Func<Employee, bool>>()).Returns(x => stub.Where((Func<Employee, bool>)x[0]));

            IEnumerable<Employee> result = _sut.FindEmployees("Empl51", "", "", "");

            Assert.AreEqual(result.Count(), 0);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void FindEmployeesMethodTest_Error()
        {
            _mockRepo.When(x => x.Find(Arg.Any<Func<Employee, bool>>())).Do(x => { throw new Exception("Test Exception"); });

            IEnumerable <Employee> result = _sut.FindEmployees("", "", "", "");

            Assert.IsNull(result);

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetEmployeeMethodTest()
        {
            List<Employee> stub = new List<Employee>()
            {
                new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" },
                new Employee() { EmployeeID = 2, Name = "Empl2", Email = "Email2", Address = "Address2", PhoneNumber = "222" },
                new Employee() { EmployeeID = 5, Name = "Empl5", Email = "Email5", Address = "Address5", PhoneNumber = "555" },
            };
            _mockRepo.GetById(5).Returns(stub[2]);

            Employee result = _sut.GetEmployee("5");

            Assert.AreSame(stub[2], result);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void GetEmployeeMethodTest_Empty()
        {
            Employee result = _sut.GetEmployee("5");

            Assert.IsNull(result);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
        }

        [Test]
        public void GetEmployeeMethodTest_Error()
        {
            _mockRepo.When(x => x.GetById(Arg.Any<int>())).Do(x => { throw new Exception("Test Exception"); });

            Employee result = _sut.GetEmployee("5");

            Assert.IsNull(result);

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }

        [Test]
        public void AddEmployeeMethodTest_ValidData()
        {
            Employee mock = new Employee() { EmployeeID = 0, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Insert(mock).Returns(3);

            _sut.AddEmployee(mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.Created);
        }

        [Test]
        public void AddEmployeeMethodTest_InvalidData()
        {
            Employee mock = new Employee() { EmployeeID = 0, Name = null, Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Insert(mock).Returns(4);

            _sut.AddEmployee(mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.BadRequest);
        }

        [Test]
        public void AddEmployeeMethodTest_Error()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.When(x => x.Insert(Arg.Any<Employee>())).Do(x => { throw new Exception("Test Exception"); });

            _sut.AddEmployee(mock);

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }

        [Test]
        public void UpdateEmployeeMethodTest_ValidData()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Update(mock).Returns(true);

            _sut.UpdateEmployee(mock.EmployeeID.ToString(), mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.NoContent);
        }

        [Test]
        public void UpdateEmployeeMethodTest_NotFound()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Update(mock).Returns(false);

            _sut.UpdateEmployee(mock.EmployeeID.ToString(), mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.NotFound);
        }

        [Test]
        public void UpdateEmployeeMethodTest_InvalidData()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "", PhoneNumber = "111555" };
            _mockRepo.Update(mock).Returns(true);

            _sut.UpdateEmployee(mock.EmployeeID.ToString(), mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.BadRequest);
        }

        [Test]
        public void UpdateEmployeeMethodTest_Error()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.When(x => x.Update(Arg.Any<Employee>())).Do(x => { throw new Exception("Test Exception"); });

            _sut.UpdateEmployee(mock.EmployeeID.ToString(), mock);

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }

        [Test]
        public void DeleteEmployeeMethodTest_Found()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Delete(1).Returns(true);

            _sut.DeleteEmployee(mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.OK);
        }

        [Test]
        public void DeleteEmployeeMethodTest_NotFound()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.Delete(1).Returns(false);

            _sut.DeleteEmployee(mock);

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.NotFound);
        }

        [Test]
        public void DeleteEmployeeMethodTest_Error()
        {
            Employee mock = new Employee() { EmployeeID = 1, Name = "Empl1", Email = "Email1", Address = "Address1", PhoneNumber = "111555" };
            _mockRepo.When(x => x.Delete(1)).Do(x => { throw new Exception("Test Exception"); });

            _sut.DeleteEmployee(mock);

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }

        [Test]
        public void DeleteEmployeeByIdMethodTest_Found()
        {
            _mockRepo.Delete(1).Returns(true);

            _sut.DeleteEmployeeById("1");

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.OK);
        }

        [Test]
        public void DeleteEmployeeByIdMethodTest_NotFound()
        {
            _mockRepo.Delete(1).Returns(false);

            _sut.DeleteEmployeeById("1");

            _mockLogger.DidNotReceive().ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.NotFound);
        }

        [Test]
        public void DeleteEmployeeByIdMethodTest_Error()
        {
            _mockRepo.When(x => x.Delete(1)).Do(x => { throw new Exception("Test Exception"); });

            _sut.DeleteEmployeeById("1");

            _mockLogger.Received(1).ErrorMethod(Arg.Any<Exception>(), Arg.Any<MethodBase>());
            _mockLogger.Received(1).TraceMethodResult(Arg.Any<MethodBase>(), HttpStatusCode.InternalServerError);
        }
    }
}
