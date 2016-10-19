using BoDi;
using Coypu;
using Coypu.Drivers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using Zukini.Examples.Features.CustomDrivers;

namespace Zukini.Examples.Features
{
    [Binding]
    public class Hooks
    {
        private readonly SessionConfiguration _sessionConfiguration;
        private readonly ZukiniConfiguration _zukiniConfiguration;
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer container, 
            SessionConfiguration sessionConfig, 
            ZukiniConfiguration zukiniConfig)
        {
            _objectContainer = container;
            _sessionConfiguration = sessionConfig;
            _zukiniConfiguration = zukiniConfig;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Retireve values from the config file is specified, if not specified, fallback to defaults
            _sessionConfiguration.Browser = GetBrowser(GetConfigValue("Browser", "firefox"));
            _sessionConfiguration.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(GetConfigValue("Timeout", "3.0")));
            _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(Convert.ToDouble(GetConfigValue("RetryInterval", "0.1")));

            // Set Zukini Specific configurations
            _zukiniConfiguration.MaximizeBrowser = Convert.ToBoolean(GetConfigValue("MaximizeBrowser", "true"));
            _zukiniConfiguration.ScreenshotDirectory = GetConfigValue("ScreenshotDirectory", "Screenshots");

            // Example of creating a custom chrome driver with specific options
            // RegisterCustomChromeBrowser();

            // Example of creating a custom firefox driver with profile options
            // RegisterCustomFirefoxBrowser();

            // Example of creating a custom chrome remote driver with options
            // IMPORTANT: Must add hub url to App.config - Grid Settings
            // RegisterCustomRemoteChromeBrowser();
        }

        /// <summary>
        /// Private method to retrieve config settings. If the config is not specified, 
        /// the defaultValue is returned instead.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        private string GetConfigValue(string key, string defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[key];
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        /// <summary>
        /// Helper method to retrieve the browser based off of the string that is passed in.
        /// </summary>
        /// <param name="browserName">Name of the browser.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        private Browser GetBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    return Browser.Firefox;
                case "chrome":
                    return Browser.Chrome;
                case "ie":
                case "internetexplorer":
                    return Browser.InternetExplorer;
                default:
                    throw new ArgumentException(String.Format("Specified browserName '{0}' is not valid.", browserName));
            }
        }

        /// <summary>
        /// Provides an example of creating and registering a custom Chrome web driver.        
        /// </summary>
        private void RegisterCustomChromeBrowser()
        {
            // create our chrome options and set a value
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("no-sandbox");

            // Pass options to a new chrome browser and pass into the BrowserSession
            var customChromeDriver = new CustomChromeSeleniumDriver(chromeOptions);
            var browserSession = new BrowserSession(customChromeDriver);

            // Finally, register with the DI container.
            _objectContainer.RegisterInstanceAs<BrowserSession>(browserSession);
        }

        /// <summary>
        /// Provides an example of how to create and register a custom Firefox driver with 
        /// a custom firefox Profile.
        /// </summary>
        private void RegisterCustomFirefoxBrowser()
        {
            // Create profile and set some settings
            // (typical scneario of specifying how to download files)
            var firefoxProfile = new FirefoxProfile();
            firefoxProfile.SetPreference("browser.download.folderList", 2);
            firefoxProfile.SetPreference("browser.download.manager.showWhenStarting", false);
            firefoxProfile.SetPreference("browser.download.dir", "C:\\Temp"); // Better to pass this in via a config value
            firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/x-gzip");

            // Pass options to a new chrome browser and pass into the BrowserSession
            var customFirefoxDriver = new CustomFirefoxSeleniumDriver(firefoxProfile);
            var browserSession = new BrowserSession(customFirefoxDriver);

            // Finally, register with the DI container.
            _objectContainer.RegisterInstanceAs<BrowserSession>(browserSession);
        }

        /// <summary>
        /// Configures a custom remote Chrome driver        
        /// </summary>
        private void RegisterCustomRemoteChromeBrowser()
        {
            // Create chrome options and add any/all arguments
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");

            // Must cast options to DesiredCapabilities due to issue with .net and driver
            // https://github.com/seleniumhq/selenium-google-code-issue-archive/issues/7043
            var capabilities = (DesiredCapabilities)options.ToCapabilities();
            capabilities.SetCapability("browserName", "chrome");

            // Pass options to a new remote chrome browser and pass into the BrowserSession
            var customRemoteChromeDriver = new CustomRemoteChromeSeleniumDriver(capabilities);
            var browserSession = new BrowserSession(customRemoteChromeDriver);

            // Finally, register with the DI container.
            _objectContainer.RegisterInstanceAs<BrowserSession>(browserSession);
        }
    }
}
