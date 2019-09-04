
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp2
{
    [TestFixture]
    class TMTests
    {
        static void Main(string[] args)
        {

        }

        IWebDriver driver;
        [SetUp]
        public void BeforeEachTestCase()
        {
            // Browser initiate 
            driver = new ChromeDriver();

            //navigate to horse-dev
            driver.Navigate().GoToUrl("http://horse-dev.azurewebsites.net/Account/Login?ReturnUrl=%2f");

            //maximize t
            driver.Manage().Window.Maximize();

            //access loginsucess method 
            // an instance of class
            LoginPage loginInstance = new LoginPage(driver);
            loginInstance.LoginSuccess();
        }

        [TearDown]
        public void AfterEachTestCase()
        {
            //Close the driver
            driver.Quit();
        }

        [Test]
        public void CreateTMnValidate()
        {
            //Class for Home page,
            // method to verify the home 
            // method to click adminstration
            // method to click time n material

            string typecode = "Material";
            string code = "Liz";
            string description = "Test";
            string price = "$1.00";

            HomePage homeInstance = new HomePage(driver);
            homeInstance.VerifyHomePage();
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();

            TimenMaterialPage tmPage = new TimenMaterialPage(driver);
            tmPage.ClickCreateNew();
            tmPage.EnterValidDataandSave(typecode, code, description, price);
            Assert.IsTrue("RecordFound" == tmPage.ValidateData(typecode, code, description, price), "Created record not found");
        }

        [Test]
        public void EditnValidate()
        {
            HomePage homeInstance = new HomePage(driver);
            homeInstance.VerifyHomePage();
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();

        }
        [Test]
        public void DeletenValidate()
        {
            HomePage homeInstance = new HomePage(driver);
            homeInstance.VerifyHomePage();
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();
        }
    }

}