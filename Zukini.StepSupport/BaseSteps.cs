using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace Zukini.StepSupport
{
    public class BaseSteps
    {
        /// <summary>
        /// Represents the Injected ObjectContainer.
        /// </summary>
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public BaseSteps(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        /// <summary>
        /// Returns the IWebDriver instance as registered with the ObjectContainer.
        /// </summary>
        protected IWebDriver Driver 
        { 
            get
            {
                return _objectContainer.Resolve<IWebDriver>();
            }
        }
    }
}
