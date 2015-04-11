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

        [Given(@"I navigate to W3Schools table reference page")]
        public void GivenINavigateToWSchoolsTableReferencePage()
        {
            Browser.Visit("http://www.w3schools.com/tags/tag_table.asp");
        }

        [Then(@"I should see that the table tag is supported in ""(.*)""")]
        public void ThenIShouldSeeThatTheTableTagIsSupportedIn(string browserName)
        {
            var page = new W3SchoolsTablePage(Browser);
            page.AssertCurrentPage();
            Assert.IsTrue(page.IsBrowserSupported(browserName), String.Format("Expected browser {0} to be supported.", browserName));
        }

    }
}
