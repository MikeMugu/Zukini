using System;
using System.Configuration;
using TechTalk.SpecFlow;
using BoDi;
using IContactPro.Test.Pages;
using Zukini.Steps;

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
            Browser.Visit(TestSettings.ApplicationUrl);

            var loginPage = new LoginPage(this.Browser);

            loginPage.AssertCurrentPage();
            loginPage.Login(TestSettings.SmokeTestUsername + "1", TestSettings.SmokeTestPassword);
        }
    }
}
