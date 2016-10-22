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
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container (Injected with DI).</param>
        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        /// <summary>
        /// Gets the object container used for dependency injection.
        /// </summary>
        protected IObjectContainer ObjectContainer
        {
            get { return _objectContainer; }
        }

        /// <summary>
        /// Global BeforeScenario hook used to new up the WebDriver instance
        /// prior to each test.
        /// </summary>
        [BeforeScenario]
        protected void BeforeScenario()
        {
            // Create a property bucket so we have a place to store values between steps
            var propertyBucket = new PropertyBucket();
            _objectContainer.RegisterInstanceAs<PropertyBucket>(propertyBucket);

            Console.WriteLine("Unique Test Id: {0}", propertyBucket.TestId);
        }
    }
}
