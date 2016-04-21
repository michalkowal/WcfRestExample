using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace WcfRestExample.E2ETests.WebElements
{
    public class EmployeeDataRow
    {
        IWebDriver _driver;
        int _rowIndex;

        public IWebElement EmployeeID
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[0];
            }
        }

        public IWebElement Name
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[1];
            }
        }

        public IWebElement Address
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[2];
            }
        }

        public IWebElement Email
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[3];
            }
        }

        public IWebElement PhoneNumber
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[4];
            }
        }

        public IWebElement EditButton
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[5].FindElement(By.ClassName("mdl-button"));
            }
        }

        public IWebElement DeleteButton
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list")).FindElements(By.TagName("tr"))[_rowIndex].FindElements(By.TagName("td"))[6].FindElement(By.ClassName("mdl-button"));
            }
        }

        public EmployeeDataRow(IWebDriver driver, int index)
        {
            _driver = driver;
            _rowIndex = index;
        }
    }

    public class EmployeesList : Index
    {
        public IWebElement List
        {
            get
            {
                return _driver.FindElement(By.Id("employees-list"));
            }
        }

        public IWebElement NewEmployeeButton
        {
            get
            {
                return _driver.FindElement(By.Id("new-employee"));
            }
        }

        public EmployeesList(IWebDriver driver, string baseUrl, WebDriverWait wait)
            : base(driver, baseUrl + "employee/", wait)
        {
        }

        public IEnumerable<EmployeeDataRow> GetDataRows()
        {
            List<EmployeeDataRow> result = new List<EmployeeDataRow>();
            IEnumerable<IWebElement> rows = List.FindElements(By.TagName("tr"));

            for (int i = 1; i < rows.Count(); i++)
            {
                result.Add(new EmployeeDataRow(_driver, i));
            }

            return result;
        }

        public EmployeeDataRow GetFirst()
        {
            EmployeeDataRow result = null;
            IEnumerable<IWebElement> rows = List.FindElements(By.TagName("tr"));

            if (rows.Count() - 1 > 0)
            {
                result = new EmployeeDataRow(_driver, 1);
            }

            return result;
        }

        public EmployeeDataRow GetLast()
        {
            EmployeeDataRow result = null;
            IEnumerable<IWebElement> rows = List.FindElements(By.TagName("tr"));

            if (rows.Count() - 1 > 0)
            {
                result = new EmployeeDataRow(_driver, rows.Count() - 1);
            }

            return result;
        }

        public void EditEmployee(EmployeeDataRow row)
        {
            row.EditButton.Click();

            Loading();
        }

        public void DeleteEmployee(EmployeeDataRow row)
        {
            row.DeleteButton.Click();

            Loading();

            _wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(By.ClassName("mdl-dialog__actions")).FindElements(By.TagName("button"))[0]));

            IWebElement button = _driver.FindElement(By.ClassName("mdl-dialog__actions")).FindElements(By.TagName("button"))[0];

            //Actions action = new Actions(_driver);
            //action = action.MoveToElement(button);

            //Loading();

            //action.Click().Perform();

            bool clicked = false;
            do
            {
                try
                {
                    button.Click();

                    clicked = true;
                }
                catch (WebDriverException e)
                {
                    continue;
                }
            } while (!clicked);

            Loading();
        }
    }
}
