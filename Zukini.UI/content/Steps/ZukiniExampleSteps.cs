using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Zukini.UI.Steps;

[Binding]
public class StepTemplate : UISteps
{
	public StepTemplate(IObjectContainer objectContainer)
		: base(objectContainer)
	{
	}

    [Given(@"I navigate to Google")]
    public void GivenINavigateToGoogle()
    {
        Browser.Visit("https://www.google.com");
    }

    [Given(@"I enter a search value of ""(.*)""")]
    public void GivenIEnterASearchValueOf(string searchValue)
    {
        var googleSearchPage = new GoogleSearchPage(Browser);
        googleSearchPage.SearchTextBox.SendKeys(searchValue);
    }

    [When(@"I press Google Search")]
    public void WhenIPressGoogleSearch()
    {
        var googleSearchPage = new GoogleSearchPage(Browser);
        googleSearchPage.SearchButton.Click();
    }

    [Then(@"I should see ""(.*)"" in the results")]
    public void ThenIShouldSeeInTheResults(string expectedValue)
    {
        Assert.IsTrue(Browser.HasContent(expectedValue));
    }

}
