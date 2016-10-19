using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace Zukini.Examples.Features.CustomDrivers
{
    public class CustomRemoteChromeSeleniumDriver : SeleniumWebDriver
    {
        public CustomRemoteChromeSeleniumDriver(DesiredCapabilities capabilities)
            : base(CustomProfileDriver(capabilities), Browser.Chrome)
        {
        }

        public static RemoteWebDriver CustomProfileDriver(DesiredCapabilities capabilities)
        {
            var uri = new Uri(TestSettings.GridUrl);
            return new RemoteWebDriver(uri, capabilities);
        }
    }
}
