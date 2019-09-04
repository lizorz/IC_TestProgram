using OpenQA.Selenium;

namespace ConsoleApp2
{
    class LoginPage
    {
        IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement firstName => driver.FindElement(By.Id("UserName"));
        IWebElement password => driver.FindElement(By.Id("Password"));
        IWebElement loginbtn => driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]"));

        //Blog about different access specifiers in c#
        internal void LoginSuccess()
        {
            // Identfying the username 
            //IWebElement firstName = driver.FindElement(By.Id("UserName"));
            firstName.SendKeys("hari");

            //Identify password 
            //IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("123123");

            // Identify Login and click
            //IWebElement loginbtn = driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]"));
            loginbtn.Click();
        }
        internal void LoginFailure()
        {
            firstName.SendKeys("sdfghari");
            password.SendKeys("12sdfsd3123");
            loginbtn.Click();
        }

    }
}
