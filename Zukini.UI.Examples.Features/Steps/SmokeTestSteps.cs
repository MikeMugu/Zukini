﻿using BoDi;
using Coypu;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI.Examples.Pages;
using Zukini.UI.Pages;
using Zukini.UI.Steps;

namespace Zukini.UI.Examples.Features.Steps
{
    [Binding]
    public class SmokeTestSteps : UISteps
    {
        private SessionConfiguration _sessionConfiguration;
        private IViewFactory _viewFactory;

        public SmokeTestSteps(IObjectContainer objectContainer, IViewFactory viewFactory, SessionConfiguration sessionConfiguration)
            : base(objectContainer)
        {
            _sessionConfiguration = sessionConfiguration;
            _viewFactory = viewFactory;
        }

        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
            Browser.WaitForNavigation(_sessionConfiguration, TestSettings.GoogleUrl);
        }

        [Given(@"I enter a search value of ""(.*)""")]
        public void GivenIEnterASearchValueOf(string searchValue)
        {
            _viewFactory.Get<GoogleSearchPage>().SearchTextBox.FillInWith(searchValue);
        }
        
        [Then(@"view factory throws an exception on attempt to load page object that is never loaded")]
        public void ThenViewFactoryThrowsAnExceptionOnAttemptToLoadDifferentPage()
        {
            Assert.Throws<ZukiniAssertionException>(() => _viewFactory.Load<MissingFakePage>());
        }
        
        [When(@"I press Google Search")]
        public void WhenIPressGoogleSearch()
        {
            _viewFactory.Get<GoogleSearchPage>().SearchButton.Click();
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
            var page = _viewFactory.Get<W3SchoolsTablePage>().AssertCurrentPage();
            Assert.IsTrue(page.IsBrowserSupported(browserName), String.Format("Expected browser {0} to be supported.", browserName));
        }

        [Given(@"I remember the sub-header text")]
        public void GivenIRememberTheSubHeaderText()
        {
            var page = _viewFactory.Get<W3SchoolsTablePage>();
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
            var page = _viewFactory.Load<W3SchoolsTablePage>().AssertCurrentPage();

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

        [Given(@"I navigate to some page(?:.*)")]
        public void GivenINavigateToSomePageWithDelayedElement()
        {
            _viewFactory.Get<FakePageObject>();
        }

        [Then(@"view factory can wait for delayed page")]
        public void ThenViewFactoryCanLoadDelayedPage()
        {
            var page = _viewFactory.Load<FakePageObject>();
            Assert.That(page.DelayedElement.Exists(), Is.True, 
                "After navigating to fake page, view factory didn't wait for delayed element");
        }

        [Then(@"I can find a youtube video component with title '(.*)'")]
        public void ThenICanSeeAVideoObjectWithTitle(string partOfTitle)
        {
            var title = _viewFactory.Load<FakePageObject>().FindPlayerByTitle(partOfTitle).Title;
            Assert.That(title.Text, Does.Contain(partOfTitle), "Wrong title");
        }

        [When(@"I click play for '(.*)' video")]
        public void WhenIClickPlayForStarWarsVideo(string title)
        {
            var player = _viewFactory.Load<FakePageObject>().FindPlayerByTitle(title);
            player.PlayButton.Click();
        }

        [Then(@"player controls appear in '(.*)' video player")]
        public void ThenPlayerControlsAppearInStarWarsVideoPlayer(string title)
        {
            var player = _viewFactory.Load<FakePageObject>().FindPlayerByTitle(title);
            Assert.That(player.Controls.Exists(), Is.True, "Controls doesn't appear after");
        }
        
        [Then(@"I can load '(.*)' gallery components with view factory")]
        public void ThenICanLoadGalleryComponentsWithViewFactory(int count)
        {
            var images = _viewFactory.Get<FakePageObject>().GalleryImages1;
            Assert.That(images.Count(), Is.EqualTo(count), "Not all images loaded");
        }

        [Then(@"I can find gallery component using view factory with title '(.*)'")]
        public void ThenICanFindGalleryComponentUsingViewFactoryWithTitle(string description)
        {
            var images = _viewFactory.Get<FakePageObject>().GalleryImages2;
            var foundImg    = images.First(img => img.Desciption.Text == description);
            Assert.That(foundImg.Image.Exists(), Is.True, "Image not found");
        }

        [Then(@"view factory throws an exception on attempt to load page component that is never loaded")]
        public void ThenExceptionAppearIfILoadComponentWithViewFactoryThatDoesNotExist()
        {
            Assert.Throws<ZukiniAssertionException>(() => 
                _viewFactory.Load(() => new MissingFakeComponent(Browser)), "Exception does not appear for view factory.");
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
