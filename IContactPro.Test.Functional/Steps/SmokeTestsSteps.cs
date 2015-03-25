using BoDi;
using IContactPro.Test.Pages;
using OpenQA.Selenium.Support.PageObjects;
using System;
using TechTalk.SpecFlow;
using Zukini.StepSupport;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace IContactPro.Test.Functional.Steps
{
    [Binding]
    public class SmokeTestsSteps : BaseSteps
    {
        public SmokeTestsSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        [Given(@"I log in to iContactPro with the smoke test account")]
        public void GivenILogInToIContactProWithTheSmokeTestAccount()
        {
            // TODO Factor out to Settings class
            var loginUrl = ConfigurationManager.AppSettings["HomeUrl"];
            this.Driver.Navigate().GoToUrl(new Uri(loginUrl));

            var loginPage = new LoginPage(this.Driver);
            PageFactory.InitElements(Driver, loginPage);

            loginPage.AssertCurrentPage();
            loginPage.Login("mwatkins@icontact.com", "Passw0rd");
        }
    }
}
