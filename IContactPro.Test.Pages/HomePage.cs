using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.PageSupport;

namespace IContactPro.Test.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) 
            : base(driver)
        {
        }

        public void AssertCurrentPage()
        {
            AssertCurrentPage("Home", By.Id("MostRecentSend"));
        }
    }
}
