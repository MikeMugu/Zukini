using BoDi;
using Coypu;
using Coypu.Drivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Zukini.Examples.Features
{
    [Binding]
    public class Hooks
    {
        private readonly SessionConfiguration _sessionConfiguration;

        public Hooks(SessionConfiguration config)
        {
            _sessionConfiguration = config;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Retireve values from the config file is specified, if not specified, fallback to defaults
            _sessionConfiguration.Browser = GetBrowser(GetConfigValue("Browser", "firefox"));
            _sessionConfiguration.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(GetConfigValue("Timeout", "3.0")));
            _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(Convert.ToDouble(GetConfigValue("RetryInterval", "0.1")));
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

    }
}
