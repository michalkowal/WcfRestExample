using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WcfRestExample.E2ETests.WebElements
{
    public class EmployeeAdd : Index
    {
        public IWebElement EmployeeForm
        {
            get
            {
                return _driver.FindElement(By.Id("employee-form"));
            }
        }

        public IWebElement Name
        {
            get
            {
                return _driver.FindElement(By.Id("name"));
            }
        }

        public IWebElement Address
        {
            get
            {
                return _driver.FindElement(By.Id("address"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return _driver.FindElement(By.Id("email"));
            }
        }

        public IWebElement PhoneNumber
        {
            get
            {
                return _driver.FindElement(By.Id("phone"));
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                return _driver.FindElement(By.Id("submit-employee"));
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                return _driver.FindElement(By.Id("cancel-employee"));
            }
        }

        public EmployeeAdd(IWebDriver driver, string baseUrl, WebDriverWait wait)
            : base(driver, baseUrl + "employee/new", wait)
        {
        }

        public void FillForm(string name, string address, string email, string phoneNumber)
        {
            Name.SendKeys(name);
            Address.SendKeys(address);
            Email.SendKeys(email);
            PhoneNumber.SendKeys(phoneNumber);
        }

        public void SaveEmployee()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButton));
            SubmitButton.Click();

            Loading();
        }

        public void Cancel()
        {
            CancelButton.Click();
        }
    }
}
