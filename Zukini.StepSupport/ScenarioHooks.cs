using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Zukini.StepSupport
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioHooks"/> class.
        /// Uses the built in DI engine (BoDi) for SpecFlow to inject the objectContainer.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ScenarioHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }


        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = new FirefoxDriver();
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        /// <summary>
        /// Global After Scenario hook used to take a screenshot (if there is an error) 
        /// and shuts down the driver.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _objectContainer.Resolve<IWebDriver>();
            if (driver != null)
            {
                if (ScenarioContext.Current.TestError != null)
                {
                    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    screenshot.SaveAsFile("TestError.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                driver.Quit();
            }
        }
    }
}
