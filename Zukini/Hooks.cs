using BoDi;
using System;
using TechTalk.SpecFlow;

namespace Zukini
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container (Injected with DI).</param>
        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        /// <summary>
        /// Gets the object container used for dependency injection.
        /// </summary>
        protected IObjectContainer ObjectContainer
        {
            get { return _objectContainer; }
        }

        /// <summary>
        /// Provides the ScenarioContext for the currently executing scenario.
        /// </summary>
        protected ScenarioContext ScenarioContext
        {
            get { return _scenarioContext; }
        }

        /// <summary>
        /// Gets the feature context for the currently executing feature.
        /// </summary>
        protected FeatureContext FeatureContext
        {
            get { return _featureContext; }
        }


        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        protected void BeforeScenario()
        {
            // Create a property bucket so we have a place to store values between steps
            var propertyBucket = _objectContainer.Resolve<PropertyBucket>();

            Console.WriteLine("Unique Test Id: {0}", propertyBucket.TestId);
        }
    }
}
