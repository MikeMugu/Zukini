using BoDi;
using Coypu;

namespace Zukini.UI.Steps
{
    public abstract class UISteps<T> : TechTalk.SpecFlow.Steps
    {
        /// <summary>
        /// A context that can contain various data for the step definition class
        /// </summary>
        protected T Context;

        /// <summary>
        /// Represents the Injected ObjectContainer.
        /// </summary>
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIStepsGeneric"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public UISteps(IObjectContainer objectContainer)
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
