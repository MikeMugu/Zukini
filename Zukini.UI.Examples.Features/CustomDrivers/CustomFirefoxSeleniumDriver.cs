using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Firefox;

namespace Zukini.UI.Examples.Features.CustomDrivers
{
    public class CustomFirefoxSeleniumDriver : SeleniumWebDriver
    {
        public CustomFirefoxSeleniumDriver(FirefoxProfile firefoxProfile)
            : base(CustomProfileDriver(firefoxProfile), Browser.Firefox)
        {
        }

        public static FirefoxDriver CustomProfileDriver(FirefoxProfile firefoxProfile)
        {
            return new FirefoxDriver(firefoxProfile);
        }
    }
}
