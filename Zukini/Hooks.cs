using BoDi;
using Coypu;
using Coypu.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
                    TakeScreenshot(browser);
                }

                browser.Dispose();
            }
        }

        /// <summary>
        /// Helper method to take a screenshot of the browser and save out to the TestResults folder.
        /// </summary>
        /// <param name="browser">BrowserSession to use for taking screenshot.</param>
        private void TakeScreenshot(BrowserSession browser)
        {
            try
            {
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestResults");
                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                string screenshotFilePath = Path.Combine(artifactDirectory, GetScreenshotName());
                browser.SaveScreenshot(screenshotFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                // TODO: Add to the report transform to interpret this as a link (XSLT - yuck)
                // Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

        /// <summary>
        /// Constructs the name of the screenshot based on the feature title, scenario title
        /// and datetime stamp.
        /// </summary>
        private string GetScreenshotName()
        {
            var feature = FeatureContext.Current.FeatureInfo.Title.Replace(" ","");
            var title = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "");
            var date = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            return String.Format("{0}_{1}_{2}_screenshot.png", feature, title, date);
        }
    }
}
