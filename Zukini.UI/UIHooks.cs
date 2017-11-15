using BoDi;
using Coypu;
using Coypu.Drivers;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace Zukini.UI
{
    [Binding]
    public class UIHooks : Hooks
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIHooks" /> class.
        /// </summary>
        /// <param name="objectContainer">The object container (Injected with DI).</param>
        /// <param name="scenarioContext">The current ScenarioContext (Injected with DI).</param>
        /// <param name="featureContext">The current FeatureContext (Injected with DI).</param>
        public UIHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext) :
            base(objectContainer, scenarioContext, featureContext)
        {
        }

        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        protected void BeforeUIScenario()
        {
            BrowserSession browser;
            SessionConfiguration sessionConfig = ObjectContainer.Resolve<SessionConfiguration>();

            // If the BrowserSession was provided, then use it.
            // Otherwise create a new session using a config (if provided)
            var providedSession = TryResolveDependency<BrowserSession>();
            if (providedSession != null)
            {
                browser = providedSession;
            }
            else
            {
                browser = sessionConfig != null ? new BrowserSession(sessionConfig) : new BrowserSession();
                ObjectContainer.RegisterInstanceAs<BrowserSession>(browser);
            }

            // Apply zukini specific settings
            if (ZukiniConfig.MaximizeBrowser)
            {
                browser.MaximiseWindow();
            }
        }

        /// <summary>
        /// Global After Scenario hook used to take a screenshot (if there is an error) 
        /// and shuts down the driver.
        /// </summary>
        [AfterScenario]
        protected void AfterUIScenario()
        {
            var browser = ObjectContainer.Resolve<BrowserSession>();
            if (browser != null)
            {
                if (this.ScenarioContext.TestError != null)
                {
                    TakeScreenshot(browser);
                }

                browser.Dispose();
            }
        }

        /// <summary>
        /// Tries to resolve a dependency from our object container and 
        /// prevents us from blowing up if it does not exist. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T TryResolveDependency<T>()
        {
            try
            {
                return ObjectContainer.Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns the ZukiniConfiguration if one was registered, otherwise returns 
        /// a new ZukiniConfiguration with default settings.
        /// </summary>
        private ZukiniUIConfiguration ZukiniConfig
        {
            get { return ObjectContainer.Resolve<ZukiniUIConfiguration>() ?? new ZukiniUIConfiguration(); }
        }

        /// <summary>
        /// Helper method to take a screenshot of the browser and save out to the TestResults folder.
        /// </summary>
        /// <param name="browser">BrowserSession to use for taking screenshot.</param>
        private void TakeScreenshot(BrowserSession browser)
        {
            try
            {
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), ZukiniConfig.ScreenshotDirectory);
                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                string screenshotFilePath = Path.Combine(artifactDirectory, GetScreenshotName());
                browser.SaveScreenshot(screenshotFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                // TODO: Add to the report transform to interpret this as a link (XSLT - yuck)
                Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

        /// <summary>
        /// Constructs the name of the screenshot based on the feature title, scenario title
        /// and test id.
        /// </summary>
        private string GetScreenshotName()
        {
            var feature = this.FeatureContext.FeatureInfo.Title.Replace(" ", "");
            var title = this.ScenarioContext.ScenarioInfo.Title.Replace(" ", "");
            var propertyBucket = ObjectContainer.Resolve<PropertyBucket>();

            var name = $"{feature}_{title}_{propertyBucket.TestId}.png";
            var finalName = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
            return finalName;
        }
    }
}
