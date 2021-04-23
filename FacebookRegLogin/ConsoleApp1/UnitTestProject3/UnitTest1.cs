using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit;
using NUnit.Framework;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;




namespace facebookLoginAutomated
{
    [TestFixture]
    public class UnitTest1
    {
       
        public IWebDriver driver;
        public WebDriverWait wait;
        public IWebElement dropdown;
       
        [SetUp]
        public void initial()
        {
             
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void RegisterTofacebook()
        {

            List<string> userRegData = new List<string> { };
            var ExcelFilePath = @"C:\Users\shaimaa.sadek\source\repos\ConsoleApp1\UnitTestProject3\FullRegData.xlsx";

            //specifying which workbook , worksheet and used rows

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    userRegData.Add(xlRange.Cells[i, j].Value2.ToString());
                }
                //defining the excel row data
                //Calling the parameters from the POM
              
                string firstName = userRegData[0];
                string SurName = userRegData[1];
                string Email = userRegData[2];
                string confirmingEmail = userRegData[3];
                string Password = userRegData[4]; 
                string DayIndex = userRegData[5];
                string MonthIndex = userRegData[6];
                string YearIndex = userRegData[7];
                string RadioGenSelec = userRegData[8];

                driver.Navigate().GoToUrl("https://www.facebook.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.FindElement(By.Name("firstname")).SendKeys(firstName);
                driver.FindElement(By.Name("lastname")).SendKeys(SurName);
                driver.FindElement(By.Name("reg_email__")).SendKeys(Email);
                driver.FindElement(By.Name("reg_passwd__")).SendKeys(Password);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.FindElement(By.Name("reg_email_confirmation__")).SendKeys(confirmingEmail);

                 driver.FindElement(By.XPath(".//*[@id="day"]/option[3]")).Click;
                 //SelectElement d= new SelectElement(dropdown);
                 //d.SelectByValue("28");

                dropdown = driver.FindElement(By.Name("birthday_month"));
                 SelectElement me = new SelectElement(dropdown);
                me.SelectByValue(MonthIndex);

                dropdown = driver.FindElement(By.Name("birthday_year"));
                SelectElement ye = new SelectElement(dropdown);
                ye.SelectByValue("1994");

                //Radio Button selection
                IList<IWebElement> rdBtn_gender = driver.FindElements(By.Name("sex"));

                //  a boolean variable which will hold the value (True/False)
                Boolean bValue = false;

                // This statement will return True, in case of first Radio button is selected
                bValue = rdBtn_gender.ElementAt(0).Selected;

                // This will check that if the bValue is True means if the first radio button is selected
                if (bValue == true)
                {
                    // This will select Second radio button, if the first radio button is selected by default
                    rdBtn_gender.ElementAt(1).Click();
                }
                else
                {
                    // If the first radio button is not selected by default, the first will be selected
                    rdBtn_gender.ElementAt(0).Click();
                }

                driver.FindElement(By.Name("websubmit")).Click();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                //assert on the confirming code sent to email message 
                var targetcodemessage = driver.FindElement(By.Id("conf_code_length_error"));
               
                NUnit.Framework.Assert.That(targetcodemessage, !Is.Null);
            }
        }
        [Test]
        public void TestsuccessfulLoginExcel()
        {
            List<string> userLoginData = new List<string> { };
            var ExcelFilePath = @"C:\Users\shaimaa.sadek\source\repos\ConsoleApp1\UnitTestProject3\testusersdata.xlsx";

            //specifying which workbook , worksheet and used rows

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    userLoginData.Add(xlRange.Cells[i, j].Value2.ToString());
                }
                //defining the excel row data
                string userName = userLoginData[2];
                string email = userLoginData[0];
                string password = userLoginData[1];

                //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                //getting the elements id  
                // NUnit.Framework.Assert.That(true, Is.True);
                driver.Navigate().GoToUrl("https://www.facebook.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.FindElement(By.Id("email")).SendKeys(email);
                driver.FindElement(By.Id("pass")).SendKeys(password);
                driver.FindElement(By.Id("loginbutton")).Click();

                //Verifying some specific elements that appear only on successful login "CreatePost" button
                var target = driver.FindElement(By.Id("creation_hub_entrypoint"));
                NUnit.Framework.Assert.That(target, !Is.Null);

            }


        }
        [Test]
        public void TestwrongpasswordLoginExcel()
        {

            List<string> userLoginData = new List<string> { };
            var ExcelFilePath = @"C:\Users\shaimaa.sadek\source\repos\ConsoleApp1\UnitTestProject3\testusersdata.xlsx";

            //specifying which workbook , worksheet and used rows

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            //Reading Row number 3
            for (int i = 3; i <= rowCount; i++)
            {
                //moving across each column
                for (int j = 1; j <= colCount; j++)
                {
                    userLoginData.Add(xlRange.Cells[i, j].Value2.ToString());

                }
                //defining the excel row data
                string userName = userLoginData[2];
                string email = userLoginData[0];
                string password = userLoginData[1];

                driver.Navigate().GoToUrl("https://www.facebook.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.FindElement(By.Name("email")).SendKeys(email);
                driver.FindElement(By.Name("pass")).SendKeys(password);
                driver.FindElement(By.Id("loginbutton")).Click();


                
                  var errorTooltip= driver.FindElement(By.CssSelector("*[role='alert']"));
                 String invalidinputalert = driver.SwitchTo().Alert().Text;

                NUnit.Framework.Assert.That(errorTooltip.Text, Does.Contain("Forgotten password?"));
                
                /*
                NUnit.Framework.Assert.That(errorTooltip.Text, Does.Contain("The email address or phone number that you've entered doesn't match any account. "));
                */




            }

          
            /*
               [TearDown]
               public void closingdriver()
               {
                   driver.Close();
               }
               */
        }

        [Test]
        public void TestwrongemailLoginExcel()
        {

        }
    }
}
