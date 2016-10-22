using BoDi;
using Coypu;
using Zukini.Steps;

namespace Zukini.UI.Steps
{
    public class UISteps : BaseSteps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UISteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public UISteps(IObjectContainer objectContainer) :
            base(objectContainer)
        {
        }

        /// <summary>
        /// Returns the IWebDriver instance as registered with the ObjectContainer.
        /// </summary>
        protected BrowserSession Browser 
        { 
            get
            {
                return ObjectContainer.Resolve<BrowserSession>();
            }
        }
    }
}
