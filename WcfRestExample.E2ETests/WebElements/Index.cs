using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace WcfRestExample.E2ETests.WebElements
{
    public class Index
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        public IWebElement LoadingBar
        {
            get
            {
                return _driver.FindElement(By.Id("loading"));
            }
        }

        public IWebElement HomeMenuButton
        {
            get
            {
                return _driver.FindElements(By.ClassName("mdl-navigation__link"))[0];
            }
        }

        public IWebElement EmployeesMenuButton
        {
            get
            {
                return _driver.FindElements(By.ClassName("mdl-navigation__link"))[1];
            }
        }

        public IWebElement ConfigurationMenuButton
        {
            get
            {
                return _driver.FindElements(By.ClassName("mdl-navigation__link"))[2];
            }
        }

        public Index(IWebDriver driver, string url, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;

            if (_driver.Url != url)
                _driver.Url = url;
        }

        public INavigation Open()
        {
            INavigation navigate = _driver.Navigate();

            Loading();

            return navigate;
        }

        public void Loading()
        {
            Thread.Sleep(250);
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("loading")));
            Thread.Sleep(500);
        }

        public void GoToHome()
        {
            HomeMenuButton.Click();

            Loading();
        }

        public void GoToEmployees()
        {
            EmployeesMenuButton.Click();

            Loading();
        }

        public void GoToConfiguration()
        {
            ConfigurationMenuButton.Click();

            Loading();
        }
    }
}
