using BoDi;
using Coypu;
using System;
using System.Collections.Generic;
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
            _sessionConfiguration.Browser = Coypu.Drivers.Browser.Firefox;
            _sessionConfiguration.Timeout = TimeSpan.FromSeconds(3);
            _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(0.1);
        }
    }
}
