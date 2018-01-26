using BoDi;
using Coypu;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI.Examples.Pages;
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
            string jsButton = "setTimeout( createButton, 1000 ); function createButton() { var button = document.createElement(\"button\"); button.innerHTML = \"I am button\"; document.getElementsByTagName(\"body\")[0].appendChild(button); }";
            Browser.ExecuteScript(jsButton);
        }

        [Then(@"the delayed button should eventually exist")]
        public void ThenTheDelayedButtonShouldEventuallyExist()
        {
            var buttons = Browser.FindAllXPath("//button");
            Assert.IsFalse(buttons.Count() > 0, "Button should not have existed yet");

            Browser.WaitUntil(() => Browser.FindAllXPath("//button").Count() > 0, "Waiting for buttons to appear");
            buttons = Browser.FindAllXPath("//button");
            Assert.IsTrue(buttons.Count() == 1, "Button should exist by now");
        }

        [Then(@"the delayed button has a size and location")]
        public void ThenTheDelayedButtonHasASizeAndLocation()
        {
            ElementScope button = Browser.FindXPath("//button");
            Assert.IsTrue(button.Rectangle().Size.Height > 1, "Button had no height");
            Assert.IsTrue(button.Rectangle().Size.Width > 1, "Button had no width");
            Assert.IsTrue(button.Rectangle().Location.X > 1, "Button had no X location");
            Assert.IsTrue(button.Rectangle().Location.Y > 1, "Button had no Y location");
        }

        [Given(@"I create a button that creates a delayed button")]
        public void GivenICreateAButtonThatCreatesADelayedButton()
        {
            string jsButton = "createButtonToClick(); function createButtonToClick() { var button = document.createElement(\"button\"); button.id = \"button1\"; button.innerHTML = \"I am button\"; button.addEventListener(\"click\", function(){ setTimeout( createSecondButton, 2000 ) }); document.getElementsByTagName(\"body\")[0].appendChild(button); } function createSecondButton() { var button = document.createElement(\"button\"); button.id = \"button2\"; button.innerHTML = \"Hello World\"; document.getElementsByTagName(\"body\")[0].appendChild(button); }";
            Browser.ExecuteScript(jsButton);
        }

        [When(@"I use TryUntil on the button")]
        public void WhenIUseTryUntilOnTheButton()
        {
            Action action = new Action(() => Browser.FindButton("button1").Click());
            Browser.TryUntil(action, () => { return Browser.FindAllXPath("//button").Count() == 2; });
        }

        [Then(@"the second button should exist")]
        public void ThenTheSecondButtonShouldExist()
        {
            var buttons = Browser.FindAllXPath("//button");
            Assert.IsTrue(buttons.Count() == 2, "Buttons should exist by now");
        }

        [Given(@"I try to navigate to Google")]
        public void GivenITryToNavigateToGoogle()
        {
            PropertyBucket.Remember("NavigationTimedOut", NavigationTimedOut(TestSettings.GoogleUrl));
        }

        [Given(@"I try to navigate to a url that changes the browser location")]
        public void GivenITryToNavigateToAUrlThatChangesTheBrowserLocation()
        {
            PropertyBucket.Remember("NavigationTimedOut", NavigationTimedOut(TestSettings.GoogleHttpUrl));
        }

        [Then(@"navigation (does|does not) timeout")]
        public void ThenNavigationWillTimeOut(string flag)
        {
            bool navigationTimedOut = PropertyBucket.GetProperty<bool>("NavigationTimedOut");
            if (flag == "does") {
                Assert.That(navigationTimedOut, Is.True, "Navigation did not timeout");
            } else {
                Assert.That(navigationTimedOut, Is.False, "Navigation timed out");
            }
        }

        private bool NavigationTimedOut(string url)
        {
            try
            {
                Browser.WaitForNavigation(_sessionConfiguration, url);
                return false;
            }
            catch (TimeoutException)
            {
                return true;
            }
        }
    }
}
