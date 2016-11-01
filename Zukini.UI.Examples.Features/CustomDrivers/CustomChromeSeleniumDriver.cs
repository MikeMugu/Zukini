using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Chrome;

namespace Zukini.UI.Examples.Features.CustomDrivers
{
    public class CustomChromeSeleniumDriver : SeleniumWebDriver
    {
        public CustomChromeSeleniumDriver(ChromeOptions chromeOptions)
            : base(CustomProfileDriver(chromeOptions), Browser.Chrome)
        {
        }

        public static ChromeDriver CustomProfileDriver(ChromeOptions chromeOptions)
        {
            return new ChromeDriver(chromeOptions);
        }

    }
}
