using LiteDB;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using WcfRestExample.Common.Data;

namespace WcfRestExample.IntegrationTests
{
    [TestFixture]
    public class EmployeeServiceIntegrationTest
    {
        private readonly Uri _employeeServiceBaseUri = new Uri("http://localhost:61256/employee/");
        private HttpClient _httpClient;

        private Employee _newEmployee1 = new Employee() { EmployeeID = 0, Name = "Test Employee 1", Address = "Test Address 1" };
        private Employee _newEmployee2 = new Employee() { EmployeeID = 0, Name = "Test Employee 2", Address = "Test Address 2", Email = "tst@email", PhoneNumber = "11-22-33" };

        private string _dbFileName;

        [OneTimeSetUp]
        public void Start()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string hostPath = Path.Combine(assemblyFolder, "..\\..\\..\\WcfRestExample.Service.Host");
            _dbFileName = Path.Combine(hostPath, ConfigurationManager.AppSettings["noSqlDb"]);
        }

        [SetUp]
        public void Init()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _employeeServiceBaseUri;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Test]
        public async Task Integrate1_AddEmployeeTest()
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", _newEmployee1);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            response = await _httpClient.PostAsJsonAsync("", _newEmployee2);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            using (LiteDatabase db = new LiteDatabase(_dbFileName))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                IEnumerable<Employee> all = col.FindAll();

                Employee dbEnt1 = all.ElementAt(all.Count() - 2);
                _newEmployee1.EmployeeID = dbEnt1.EmployeeID;
                AssertEx.PropertyValuesAreEquals(_newEmployee1, dbEnt1);

                Employee dbEnt2 = all.ElementAt(all.Count() - 1);
                _newEmployee2.EmployeeID = dbEnt2.EmployeeID;
                AssertEx.PropertyValuesAreEquals(_newEmployee2, dbEnt2);
            }
        }

        [Test]
        public async Task Integrate2_GetAllEmployeesTest()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            IEnumerable<Employee> all = await response.Content.ReadAsAsync<IEnumerable<Employee>>();

            AssertEx.PropertyValuesAreEquals(_newEmployee1, all.ElementAt(all.Count() - 2));
            AssertEx.PropertyValuesAreEquals(_newEmployee2, all.ElementAt(all.Count() - 1));
        }

        [Test]
        public async Task Integrate3_GetEmployeeTest()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_newEmployee2.EmployeeID.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Employee emplEnt = await response.Content.ReadAsAsync<Employee>();

            Assert.IsNotNull(emplEnt);

            AssertEx.PropertyValuesAreEquals(_newEmployee2, emplEnt);
        }

        [Test]
        public async Task Integrate4_UpdateEmployeeTest()
        {
            _newEmployee1.Name = "New Name";
            _newEmployee1.PhoneNumber = "555-555-111";

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_newEmployee1.EmployeeID.ToString(), _newEmployee1);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            using (LiteDatabase db = new LiteDatabase(_dbFileName))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                Employee dbEmpl = col.FindById(_newEmployee1.EmployeeID);

                Assert.IsNotNull(dbEmpl);

                AssertEx.PropertyValuesAreEquals(_newEmployee1, dbEmpl);
            }
        }

        [Test]
        public async Task Integrate5_DeleteEmployeeTest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, _newEmployee2.EmployeeID.ToString());
            request.Content = new ObjectContent<Employee>(_newEmployee2, new JsonMediaTypeFormatter());
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (LiteDatabase db = new LiteDatabase(_dbFileName))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                Employee dbEmpl = col.FindById(_newEmployee2.EmployeeID);

                Assert.IsNull(dbEmpl);
            }
        }

        [Test]
        public async Task Integrate6_DeleteEmployeeByIdTest()
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(_newEmployee1.EmployeeID.ToString());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (LiteDatabase db = new LiteDatabase(_dbFileName))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                Employee dbEmpl = col.FindById(_newEmployee1.EmployeeID);

                Assert.IsNull(dbEmpl);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
