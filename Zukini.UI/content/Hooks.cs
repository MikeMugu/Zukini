using System;
using Coypu;
using Coypu.Drivers;
using TechTalk.SpecFlow;

[Binding]
public class Hooks
{
    private readonly SessionConfiguration _sessionConfiguration;

    public Hooks(SessionConfiguration sessionConfig)
    {
        _sessionConfiguration = sessionConfig;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _sessionConfiguration.Browser = Browser.Chrome;
        _sessionConfiguration.Timeout = TimeSpan.FromSeconds(3);
        _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(0.1);        
    }
}
