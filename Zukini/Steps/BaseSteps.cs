using BoDi;
using Coypu;
using TechTalk.SpecFlow;

namespace Zukini.Steps
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
        protected BrowserSession Browser 
        { 
            get
            {
                return _objectContainer.Resolve<BrowserSession>();
            }
        }
    }
}
