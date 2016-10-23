using BoDi;
using Zukini.Steps;

namespace Zukini.API.Steps
{
    public abstract class ApiSteps : BaseSteps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ApiSteps(IObjectContainer objectContainer) :
            base(objectContainer)
        {
        }

    }
}
