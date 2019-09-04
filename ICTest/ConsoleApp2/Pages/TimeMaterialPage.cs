using OpenQA.Selenium;
using System;
using System.Threading;
using NUnit.Framework;
using System.Linq;

namespace ConsoleApp2
{
    internal class TimenMaterialPage
    {
        IWebDriver driver;
        public TimenMaterialPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        internal void ClickCreateNew()
        {
            //Click create new button
            driver.FindElement(By.XPath("//a[contains(.,'Create New')]")).Click();
        }

        internal void EnterValidDataandSave(string typecode, string code, string description, string price)
        {
            SelectTypeCode(typecode);
            //Enter code 
            driver.FindElement(By.Id("Code")).SendKeys(code);

            // Enterd description
            driver.FindElement(By.Id("Description")).SendKeys(description);

            //Price per unit
            driver.FindElement(By.XPath("//input[contains(@class,'k-formatted-value k-input')]")).SendKeys(price);

            // click save
            driver.FindElement(By.Id("SaveButton")).Click();

            ////click save
            //driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        internal string ValidateData(string typecode, string code, string desc, string price)
        {
            int result = SearchRecord(typecode, code, desc, price);
            if (result > 0) { return "RecordFound"; }
            return "RecordNotFound";
        }

        internal string DeleteData(string typecode, string code, string desc, string price, string alertkey = "ok")
        {
            int colnumber = SearchRecord(typecode, code, desc, price);
            if (colnumber < 1) { return "RecordNotFound"; }

            // Click delete button
            string deletebtn = String.Format("((//a[@class='k-button k-button-icontext k-grid-Delete'][contains(.,'Delete')])[{0}])", colnumber);
            driver.FindElement(By.XPath(deletebtn)).Click();

            if (alertkey == "ok")
            {
                driver.SwitchTo().Alert().Accept();
                Console.WriteLine("record deleted");
            }
            else if (alertkey == "cancel")
            {
                driver.SwitchTo().Alert().Dismiss();
            }
            return "success";
        }

        internal string EditValidDataandSave(string typecode, string code, string desc, string price, string newtypecode, string newcode, string newdesc, string newprice)
        {
            int colnumber = SearchRecord(typecode, code, desc, price);
            if (colnumber < 0) { return "RecordNotFound"; }

            // Click edit button
            string editbtn = String.Format("(//a[@class='k-button k-button-icontext k-grid-Edit'][contains(.,'Edit')])[{0}]", colnumber);
            driver.FindElement(By.XPath(editbtn)).Click();

            // Select Time/Material TypeCode dropdown
            SelectTypeCode(newtypecode);

            //Enter code
            if (newcode != null)
            {
                IWebElement Code = driver.FindElement(By.Id("Code"));
                Code.Clear();
                Code.SendKeys(newcode);
            }

            // Enter description
            if (newdesc != null)
            {
                IWebElement Description = driver.FindElement(By.Id("Description"));
                Description.Clear();
                Description.SendKeys(newdesc);
            }

            // Enter Price
            if (newprice != null)
            {
                IWebElement price1 = driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[4]/div/span[1]/span/input[1]"));
                price1.Clear();
                IWebElement Price = driver.FindElement(By.Id("Price"));
                Price.SendKeys(newprice);
            }

            // click save
            driver.FindElement(By.Id("SaveButton")).Click();
            return "success";
        }
        internal void SelectTypeCode(string typecode)
        {
            driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[1]/div/span[1]/span/span[1]")).Click();
            IWebElement ele = driver.FindElement(By.Id("TypeCode_listbox"));
            var options = ele.FindElements(By.TagName("li"));
            if (typecode != null)
            {
                foreach (var opt in options)
                {
                    if (opt.Text == typecode)
                    {
                        opt.Click();
                        break;
                    }
                };
            }
        }
        internal void SelectListCode(string listcode)
        {
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/span[1]/span/span/span[1]")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/div[5]/div/ul/li[3]")).Click();
            //var options = ele.FindElements(By.TagName("li"));
            //Console.WriteLine("step 1 " + listcode);
            //if (listcode != null)
            //{
            //    Console.WriteLine("step 2 " + listcode);
            //    foreach (var opt in options)
            //    {
            //        Console.WriteLine("step 3 " + opt+" "+listcode);
            //        if (opt.Text == listcode)
            //        {
            //            Console.WriteLine("step 4 " + opt + " " + listcode);

            //            opt.Click();
            //            break;
            //        }
            //    };
            //}
        }

        internal string TypeCodeInTable(string typecode)
        {
            if (typecode == "Material" || typecode == "M" || typecode == null) { typecode = "M"; }
            if (typecode == "Time" || typecode == "T") { typecode = "T"; }
            return typecode;
        }
        internal int SearchRecord(string typecode, string code, string desc, string price)
        {
            try
            {
                IWebElement table = Wait.ElementIsVisible(driver, "//*[@id=\"tmsGrid\"]/div[3]/table", 10);
                SelectListCode("20");
                typecode = TypeCodeInTable(typecode);
                while (true)
                {
                    var rows = table.FindElements(By.TagName("tr"));
                    int colnumber = 1;
                    foreach (var row in rows)
                    {
                        var columns = row.FindElements(By.TagName("td")).ToList();
                        if ((columns[0].Text == code) && (columns[1].Text == typecode) && (columns[2].Text == desc) && (columns[3].Text == price))
                        { return colnumber; }
                        colnumber++;
                    }
                    // Navigate to next page until the next button is disabled, otherwise the records in last page are read again and again
                    if (!driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[3]")).GetAttribute("class").Contains("k-state-disabled"))
                    {
                        driver.FindElement(By.XPath("//span[contains(.,'Go to the next page')]")).Click();
                    }
                    else
                    {
                        return -1;
                    }

                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }
    }
}
