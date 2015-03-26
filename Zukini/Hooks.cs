using BoDi;
using Coypu;
using Coypu.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Zukini
{
    [Binding]
    public class Hooks
    {
        private IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container (Injected with DI).</param>
        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        protected void BeforeScenario()
        {
            // TODO: Pass SessionConfiguration to BrowserSession
            var browser = new BrowserSession();
            _objectContainer.RegisterInstanceAs<BrowserSession>(browser);
        }

        /// <summary>
        /// Global After Scenario hook used to take a screenshot (if there is an error) 
        /// and shuts down the driver.
        /// </summary>
        [AfterScenario]
        protected void AfterScenario()
        {
            var browser = _objectContainer.Resolve<BrowserSession>();
            if (browser != null)
            {
                if (ScenarioContext.Current.TestError != null)
                {
                    browser.SaveScreenshot("TestError.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                browser.Dispose();
            }
        }
    }
}
