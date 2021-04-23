using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string firstname = "Suresh";
     
            string lastname = "Dasari";
            Console.WriteLine(lastname+""+lastname); */
        }
        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void STARTTC()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void Login()
        {
            driver.Navigate().GoToUrl("www.facebook.com");
            driver.FindElement(By.Name("email")).SendKeys("Mary Alddbbgbicjga Shepardsen");
            driver.FindElement(By.Name("Pass")).SendKeys("Testing24020738");
            driver.FindElement(By.Name("loginbutton")).Click();
        }


        [TearDown]
        public void Endtc()
        {
        }

    }

}
