using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace ConsoleApp2
{
    [Binding]
    public class ModifyValues
    {
        IWebDriver driver;
        [Given(@"Logged into the turn up portal")]
        public void LoggedIntoTheTurnUpPortal()
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

        [Given(@"Navigate to the time and material page")]
        public void NavigateToTheTimeAndMaterialPage()
        {
            HomePage homeInstance = new HomePage(driver);
            homeInstance.VerifyHomePage();
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();
        }

        [Then(@"Create a time and material record.")]

        public void CreateATimeAndMaterialRecord()
        {

            string typecode = "Material";
            string code = "Liz";
            string description = "Test";
            string price = "$1.00";

            TimenMaterialPage tmPage = new TimenMaterialPage(driver);
            tmPage.ClickCreateNew();
            tmPage.EnterValidDataandSave(typecode, code, description, price);
            Assert.IsTrue("RecordFound" == tmPage.ValidateData(typecode, code, description, price), "Created record not found");

            driver.Quit();
        }

        [Then(@"Delete a time and material record.")]
        public void DeleteATimeAndMaterialRecord()
        {
            TimenMaterialPage tmPage = new TimenMaterialPage(driver);
            HomePage homeInstance = new HomePage(driver);

            string typecode = "Material";
            string code = "Liz";
            string description = "Test";
            string price = "$1.00";

            tmPage.ClickCreateNew();
            tmPage.EnterValidDataandSave(typecode, code, description, price);
            tmPage.DeleteData(typecode, code, description, price);

            // Verifies if the record is deleted
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();
            Assert.IsTrue("RecordNotFound" == tmPage.ValidateData(typecode, code, description, price), "Validate Failed");

            driver.Quit();
        }

        [Then(@"Edit a time and material record.")]
        public void EditATimeAndMaterialRecord()
        {
            HomePage homeInstance = new HomePage(driver);
            homeInstance.VerifyHomePage();
            homeInstance.ClickAdminstration();
            homeInstance.ClickTimenMaterial();

            string typecode = "Material";
            string code = "Liz";
            string description = "Test";
            string price = "$1.00";
            string newtypecode = "Material";
            string newcode = "Liz";
            string newdescription = "Test2";
            string newprice = "$0.99";

            //Console.WriteLine(typecode + code + description + price + newtypecode + newdescription + newprice);

            TimenMaterialPage tmPage = new TimenMaterialPage(driver);
            tmPage.ClickCreateNew();
            tmPage.EnterValidDataandSave(typecode, code, description, price);
            string result = tmPage.EditValidDataandSave(typecode, code, description, price, newtypecode, newcode, newdescription, newprice);
            Assert.IsTrue("success" == result, "Edit failed");
            Assert.IsTrue("RecordFound" == tmPage.ValidateData(newtypecode, newcode, newdescription, newprice), "Validate failed");
            driver.Quit();
        }
    }
}
