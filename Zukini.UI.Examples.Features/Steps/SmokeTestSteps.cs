using BoDi;
using Coypu;
using Coypu.Actions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using TechTalk.SpecFlow;
using Zukini.UI.Examples.Pages;
using Zukini.UI.ExtensionMethods;
using Zukini.UI.Steps;

namespace Zukini.UI.Examples.Features.Steps
{
    [Binding]
    public class SmokeTestSteps : UISteps
    {
        private SessionConfiguration _sessionConfiguration;

        public SmokeTestSteps(IObjectContainer objectContainer, SessionConfiguration sessionConfiguration)
            : base(objectContainer)
        {
            _sessionConfiguration = sessionConfiguration;
        }

        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
            Browser.WaitForNavigation(_sessionConfiguration, TestSettings.GoogleUrl);
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
            Browser.WaitForNavigation(_sessionConfiguration, TestSettings.W3SchoolsBaseUrl + "/tags/tag_table.asp");
        }

        [Then(@"I should see that the table tag is supported in ""(.*)""")]
        public void ThenIShouldSeeThatTheTableTagIsSupportedIn(string browserName)
        {
            var page = new W3SchoolsTablePage(Browser);
            page.AssertCurrentPage();
            Assert.IsTrue(page.IsBrowserSupported(browserName), String.Format("Expected browser {0} to be supported.", browserName));
        }

        [Given(@"I remember the sub-header text")]
        public void GivenIRememberTheSubHeaderText()
        {
            var page = new W3SchoolsTablePage(Browser);
            var divText = page.TopTextDiv.Text;
            PropertyBucket.Remember("W3SchoolsHeaderText", divText);
        }

        [Then(@"the sub-header text should have been ""(.*)""")]
        public void ThenTheSubHeaderTextShouldHaveBeen(string headerText)
        {
            string rememberedText = PropertyBucket.GetProperty<string>("W3SchoolsHeaderText");
            Assert.AreEqual(headerText, rememberedText);
        }

        [Then(@"I should see that the table tag is supported for the following")]
        public void ThenIShouldSeeThatTheTableTagIsSupportedForTheFollowing(Table table)
        {
            var page = new W3SchoolsTablePage(Browser);
            page.AssertCurrentPage();

            // Iterate through the table and verify support for each browser
            foreach(TableRow row in table.Rows)
            {
                string browserName = row["Browser"];
                Assert.IsTrue(page.IsBrowserSupported(browserName), $"Expected browser {browserName} to be supported.");
            }
        }

        [Given(@"I create a delayed button")]
        public void GivenICreateADelayedButton()
        {
            string jsButton = Properties.Resources.ResourceManager.GetString("jsDelayedButton");
            Browser.ExecuteScript(jsButton);
        }

        [Then(@"the delayed button should eventually exist")]
        public void ThenTheDelayedButtonShouldEventuallyExist()
        {
            var buttons = Browser.FindAllXPath("//button");
            Assert.IsFalse(buttons.Count() > 0, "Button should not have existed yet");

            Browser.WaitUntil(() => Browser.FindAllXPath("//button").Count() > 0);
            buttons = Browser.FindAllXPath("//button");
            Assert.IsTrue(buttons.Count() == 1, "Button should exist by now");
        }

        [Given(@"I create a button that creates a delayed button")]
        public void GivenICreateAButtonThatCreatesADelayedButton()
        {
            string jsButton = Properties.Resources.ResourceManager.GetString("jsButtonCreatesDelayedButton");
            Browser.ExecuteScript(jsButton);
        }

        [When(@"I use TryUntil on the button")]
        public void WhenIUseTryUntilOnTheButton()
        {
            var buttons = Browser.FindAllXPath("//button");
            Assert.IsTrue(buttons.Count() == 1, "Only one button should exist");

            Action action = new Action(() => Browser.FindButton("button1").Click());
            Browser.TryUntil(action, () => { return Browser.FindAllXPath("//button").Count() == 2; });
        }

        [Then(@"the second button should exist")]
        public void ThenTheSecondButtonShouldExist()
        {
            var buttons = Browser.FindAllXPath("//button");
            Assert.IsTrue(buttons.Count() == 2, "Buttons should exist by now");
        }
    }
}
