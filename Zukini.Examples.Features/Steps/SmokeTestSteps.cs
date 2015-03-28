using BoDi;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Zukini.Examples.Pages;
using Zukini.Steps;

namespace Zukini.Examples.Features.Steps
{
    [Binding]
    public class SmokeTestSteps : BaseSteps
    {
        public SmokeTestSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
            Browser.Visit("http://www.google.com");
        }

        [Given(@"I enter a search value of ""(.*)""")]
        public void GivenIEnterASearchValueOf(string searchValue)
        {
            new GoogleSearchPage(Browser).SearchTextBox.FillInWith(searchValue);
        }

        [When(@"I press Google Search")]
        public void WhenIPressGoogleSearch()
        {
            new GoogleSearchPage(Browser).SearchButton.Click();
        }

        [Then(@"I should see ""(.*)"" in the results")]
        public void ThenIShouldSeeInTheResults(string searchResultMatch)
        {
            Assert.IsTrue(Browser.HasContent(searchResultMatch));
        }
    }
}
