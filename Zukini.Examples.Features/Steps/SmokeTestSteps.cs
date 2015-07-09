using BoDi;
using Coypu;
using Coypu.NUnit.Matchers;
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
            Browser.Visit(TestSettings.GoogleUrl);
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
            Browser.Visit(TestSettings.W3SchoolsBaseUrl + "/tags/tag_table.asp");
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


        [Given(@"I am on the WWF site")]
        public void GivenIAmOnTheWWFSite()
        {
            Browser.Visit(TestSettings.WWFUrl);
        }

        [Then(@"I should see links for ""(.*)"" and ""(.*)"" and ""(.*)"" in the header")]
        public void ThenIShouldSeeLinksForAndAndInTheHeader(string OURWORK, string SPECIES, string PLACES)
        {
            var p = new WWFPage(Browser);
            Assert.That(p.Header, Shows.Content(OURWORK));
            Assert.That(p.Header, Shows.Content(SPECIES));
            Assert.That(p.Header, Shows.Content(PLACES));
        }
       

        [StepDefinition(@"I change my Viewport to (.*) and (.*)")]
        public void WhenIChangeMyViewportToAnd(int w, int h)
        {
            Browser.ResizeTo(w, h);
        }

        [Then(@"I should see a Menu Icon")]
        public void ThenIShouldSeeAMenuIcon()
        {
            var p = new WWFPage(Browser);
            Assert.True(p.MenuIcon.Exists());
        }


        [Then(@"I should see the Story Text and the Story Images lined up horizontally")]
        public void ThenIShouldSeeTheStoryTextAndTheStoryImagesLinedUpHorizontally()
        {
            var p = new WWFPage(Browser);
            new ResponsiveHelper(Browser).AAndBLineUp(p.Text, p.Image);

        }

        [Then(@"I should see the Image appear beneath its associated Story text")]
        public void ThenIShouldSeeTheImageAppearBeneathItsAssociatedStoryText()
        {
            var p = new WWFPage(Browser);
            new ResponsiveHelper(Browser).ABeforeB(p.Text, p.Image);
        }

      

    }
}
