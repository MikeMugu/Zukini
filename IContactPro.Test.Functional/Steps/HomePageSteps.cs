using BoDi;
using IContactPro.Test.Pages;
using System;
using TechTalk.SpecFlow;
using Zukini.Steps;

namespace IContactPro.Test.Functional.Steps
{
    [Binding]
    public class HomePageSteps : BaseSteps
    {
        public HomePageSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        [Then(@"I should be on the Home page")]
        public void ThenIShouldBeOnTheHomePage()
        {
            new HomePage(this.Browser).AssertCurrentPage();
        }
    }
}
