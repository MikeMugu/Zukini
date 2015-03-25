using BoDi;
using System;
using TechTalk.SpecFlow;
using Zukini.StepSupport;

namespace IContactPro.Test.Functional.Steps
{
    [Binding]
    public class NavigationSteps : BaseSteps
    {
        public NavigationSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        { 
        }

        [Then(@"I navigate to the contacts page")]
        public void ThenINavigateToTheContactsPage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
