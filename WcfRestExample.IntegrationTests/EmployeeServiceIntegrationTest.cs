using LiteDB;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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

        [OneTimeSetUp]
        public void Start()
        {
            string dbFileName = ConfigurationManager.AppSettings["noSqlDb"];
            System.IO.File.Copy(dbFileName, dbFileName + ".tmp", true);
            System.IO.File.Delete(dbFileName);
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

            using (LiteDatabase db = new LiteDatabase(ConfigurationManager.AppSettings["noSqlDb"]))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                IEnumerable<Employee> all = col.FindAll();

                Assert.AreEqual(2, all.Count());

                Employee dbEnt1 = all.ElementAt(0);
                _newEmployee1.EmployeeID = dbEnt1.EmployeeID;
                AssertEx.PropertyValuesAreEquals(_newEmployee1, dbEnt1);

                Employee dbEnt2 = all.ElementAt(1);
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

            Assert.AreEqual(2, all.Count());

            AssertEx.PropertyValuesAreEquals(_newEmployee1, all.ElementAt(0));
            AssertEx.PropertyValuesAreEquals(_newEmployee2, all.ElementAt(1));
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

            using (LiteDatabase db = new LiteDatabase(ConfigurationManager.AppSettings["noSqlDb"]))
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

            using (LiteDatabase db = new LiteDatabase(ConfigurationManager.AppSettings["noSqlDb"]))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                IEnumerable<Employee> all = col.FindAll();

                Assert.AreEqual(1, all.Count());

                Assert.AreNotEqual(_newEmployee2.EmployeeID, all.First().EmployeeID);
            }
        }

        [Test]
        public async Task Integrate6_DeleteEmployeeByIdTest()
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(_newEmployee1.EmployeeID.ToString());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (LiteDatabase db = new LiteDatabase(ConfigurationManager.AppSettings["noSqlDb"]))
            {
                LiteCollection<Employee> col = db.GetCollection<Employee>("Employee");

                IEnumerable<Employee> all = col.FindAll();

                Assert.AreEqual(0, all.Count());
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

        [OneTimeTearDown]
        public void End()
        {
            string dbFileName = ConfigurationManager.AppSettings["noSqlDb"];
            System.IO.File.Copy(dbFileName + ".tmp", dbFileName, true);
            System.IO.File.Delete(dbFileName + ".tmp");
        }
    }
}
