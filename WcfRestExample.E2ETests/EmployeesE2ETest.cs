using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using WcfRestExample.E2ETests.WebElements;

namespace WcfRestExample.E2ETests
{
    [TestFixture]
    public class EmployeesE2ETest
    {
        string BASE_URL = "http://localhost:51042/#/";
        IWebDriver _driver;
        WebDriverWait _wait;

        [OneTimeSetUp]
        public void Init()
        {
            _driver = new FirefoxDriver();
        }

        [SetUp]
        public void InitTest()
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void E2E1_AddEmployee()
        {
            Index indexPage = new Index(_driver, BASE_URL, _wait);
            INavigation navigation = indexPage.Open();

            indexPage.GoToEmployees();

            EmployeesList listPage = new EmployeesList(_driver, BASE_URL, _wait);

            listPage.NewEmployeeButton.Click();
            listPage.Loading();

            EmployeeAdd addPage = new EmployeeAdd(_driver, BASE_URL, _wait);

            addPage.FillForm("Test Employee", "Test Address", "test@email", "123-456-789");

            addPage.SaveEmployee();

            EmployeeDataRow newEmployee = listPage.GetLast();

            listPage.Loading();

            Assert.AreEqual("Test Employee", newEmployee.Name.Text);
            Assert.AreEqual("Test Address", newEmployee.Address.Text);
            Assert.AreEqual("test@email", newEmployee.Email.Text);
            Assert.AreEqual("123-456-789", newEmployee.PhoneNumber.Text);
        }

        [Test]
        public void E2E2_EditEmployee()
        {
            Index indexPage = new Index(_driver, BASE_URL, _wait);
            indexPage.Open();

            indexPage.GoToEmployees();

            EmployeesList listPage = new EmployeesList(_driver, BASE_URL, _wait);
            EmployeeDataRow employee = listPage.GetLast();

            listPage.Loading();

            string employeeId = employee.EmployeeID.Text;
            Assert.False(string.IsNullOrEmpty(employeeId));
            Assert.AreEqual("Test Employee", employee.Name.Text);
            Assert.AreEqual("Test Address", employee.Address.Text);
            Assert.AreEqual("test@email", employee.Email.Text);
            Assert.AreEqual("123-456-789", employee.PhoneNumber.Text);

            listPage.EditEmployee(employee);

            EmployeeEdit editPage = new EmployeeEdit(_driver, BASE_URL, employeeId, _wait);
            editPage.Email.Clear();
            editPage.Email.SendKeys("newtest@email");
            editPage.PhoneNumber.Clear();
            editPage.PhoneNumber.SendKeys("123-456-789new");

            editPage.SaveEmployee();

            listPage = new EmployeesList(_driver, BASE_URL, _wait);
            employee = listPage.GetLast();

            listPage.Loading();

            Assert.AreEqual("newtest@email",employee.Email.Text);
            Assert.AreEqual("123-456-789new", employee.PhoneNumber.Text);
        }

        [Test]
        public void E2E3_DeleteEmployee()
        {
            Index indexPage = new Index(_driver, BASE_URL, _wait);
            indexPage.Open();

            indexPage.GoToEmployees();

            EmployeesList listPage = new EmployeesList(_driver, BASE_URL, _wait);
            IEnumerable<EmployeeDataRow> allEmployees = listPage.GetDataRows();

            listPage.Loading();

            int numberOfEmployees = allEmployees.Count();

            Assert.Greater(numberOfEmployees, 0);

            EmployeeDataRow employee = allEmployees.Last();

            Assert.False(string.IsNullOrEmpty(employee.EmployeeID.Text));
            Assert.AreEqual("Test Employee", employee.Name.Text);
            Assert.AreEqual("Test Address", employee.Address.Text);
            Assert.AreEqual("newtest@email", employee.Email.Text);
            Assert.AreEqual("123-456-789new", employee.PhoneNumber.Text);

            listPage.DeleteEmployee(employee);

            listPage = new EmployeesList(_driver, BASE_URL, _wait);
            allEmployees = listPage.GetDataRows();

            listPage.Loading();

            Assert.AreEqual(numberOfEmployees - 1, allEmployees.Count());
        }

        [TearDown]
        public void End()
        {
            
        }

        [OneTimeTearDown]
        public void Quit()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}
