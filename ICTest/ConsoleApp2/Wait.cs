using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ConsoleApp2
{
    class Wait
    {
        public static IWebElement ElementIsVisible(IWebDriver driver, string value, int seconds = 3, string key = "XPath")
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
            if (key == "XPath")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(value)));
                return element;
            }
            if (key == "ID")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(value)));
                return element;
            }
            if (key == "ClassName")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(value)));
                return element;
            }

            return null;


        }

    }
}
