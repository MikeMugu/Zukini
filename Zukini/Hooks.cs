using BoDi;
using Coypu;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace Zukini
{
    [Binding]
    public class Hooks
    {
        private IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container (Injected with DI).</param>
        /// <param name="scenarioContext">The scenario context (Injected with DI).</param>
        /// <param name="featureContext">The feature context (Injected with DI).</param>
        /// With parallel test execution we cannot use static properties of contexts so we must
        /// inject them: http://www.specflow.org/documentation/Parallel-Execution/
        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            if (scenarioContext == null) throw new ArgumentNullException("ScenarioContext");
            _scenarioContext = scenarioContext;
            if (featureContext == null) throw new ArgumentNullException("FeatureContext");
            _featureContext = featureContext;
        }

        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        protected void BeforeScenario()
        {
            BrowserSession browser;
            SessionConfiguration config = _objectContainer.Resolve<SessionConfiguration>();

            // If the BrowserSession was provided, then use it.
            // Otherwise create a new session using a config (if provided)
            var providedSession = TryResolveDependency<BrowserSession>();
            if (providedSession != null)
            {
                browser = providedSession;
            }
            else
            {
                browser = config != null ? new BrowserSession(config) : new BrowserSession();
                _objectContainer.RegisterInstanceAs<BrowserSession>(browser);
            }

            // Apply zukini specific settings
            if (ZukiniConfig.MaximizeBrowser)
            {
                browser.MaximiseWindow();
            }

            // Create a property bucket so we have a place to store values between steps
            var propertyBucket = new PropertyBucket();
            _objectContainer.RegisterInstanceAs<PropertyBucket>(propertyBucket);

            Console.WriteLine("Unique Test Id: {0}", propertyBucket.TestId);
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
                if(_scenarioContext.TestError != null)
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
        private T TryResolveDependency<T>()
        {
            try
            {
                return _objectContainer.Resolve<T>();
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
        private ZukiniConfiguration ZukiniConfig
        {
            get { return _objectContainer.Resolve<ZukiniConfiguration>() ?? new ZukiniConfiguration(); }
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
            var feature = _featureContext.FeatureInfo.Title.Replace(" ","");
            var title = _scenarioContext.ScenarioInfo.Title.Replace(" ","");
            var propertyBucket = _objectContainer.Resolve<PropertyBucket>();

            return String.Format("{0}_{1}_{2}.png", feature, title, propertyBucket.TestId);
        }
    }
}
